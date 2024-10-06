using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using QuantumQuasars.ScriptableObjects;
using System.Collections.Generic;
using QuantumQuasars.Enums;
using System.Linq;
using TMPro;
using QuantumQuasars.Data;
using QuantumQuasars.Keys;

namespace QuantumQuasars.Manager
{
    public class QuizManager : MonoBehaviour
    {
        [SerializeField] private GameObject topicItemPrefab;
        [SerializeField] private GameObject topicsPanel;
        [SerializeField] private GameObject difficultyPanel;
        [SerializeField] private GameObject quizPanel;
        [SerializeField] private GameObject quizResultPanel;

        [Header("Buttons")]
        [SerializeField] private Button basicB;
        [SerializeField] private Button intermediateB;
        [SerializeField] private Button advancedB;


        [Header("Buttons")]
        [SerializeField] private GameObject endQuizB;
        [SerializeField] private GameObject showResultB;

        [Header("Quiz Result")]
        [SerializeField] private TextMeshProUGUI resultLabel;

        [Header("Quiz Panel")]
        [SerializeField] private TextMeshProUGUI questionLabel;
        [SerializeField] private Button optionA, optionB, optionC, optionD;
        [SerializeField] private Button preB, nextB;
        private bool quizEnd;
        public int curQIndex;
        public List<QuestionData> currentQuestions = new();

        private TopicSO selectedTopic;
        private Difficulty selectedDifficulty;

        private readonly List<GameObject> _createdTopics = new();
        private readonly List<GameObject> _createdDifficulties = new();

        private void Start()
        {
            OpenTopics();
        }

        private void OpenDifficulties(TopicSO item)
        {
            selectedTopic = item;
            topicsPanel.SetActive(false);
            difficultyPanel.SetActive(true);
            quizPanel.SetActive(false);
            quizResultPanel.SetActive(false);

            switch (selectedTopic.name)
            {
                case "topic 01":
                    intermediateB.interactable = PlayerPrefs.GetInt(SaveKeys.TOPIC_1_INTERMEDIATE, 0) == 1;
                    advancedB.interactable = PlayerPrefs.GetInt(SaveKeys.TOPIC_1_ADVANCED, 0) == 1;
                    break;
                case "topic 02":
                    basicB.interactable = PlayerPrefs.GetInt(SaveKeys.TOPIC_2_EASY, 0) == 1;
                    intermediateB.interactable = PlayerPrefs.GetInt(SaveKeys.TOPIC_2_INTERMEDIATE, 0) == 1;
                    advancedB.interactable = PlayerPrefs.GetInt(SaveKeys.TOPIC_2_ADVANCED, 0) == 1;
                    break;
                case "topic 03":
                    basicB.interactable = PlayerPrefs.GetInt(SaveKeys.TOPIC_3_EASY, 0) == 1;
                    intermediateB.interactable = PlayerPrefs.GetInt(SaveKeys.TOPIC_3_INTERMEDIATE, 0) == 1;
                    advancedB.interactable = PlayerPrefs.GetInt(SaveKeys.TOPIC_3_ADVANCED, 0) == 1;
                    break;
            }
        }

        public void StartQuiz(int value)
        {
            selectedDifficulty = (Difficulty)value;
            topicsPanel.SetActive(false);
            difficultyPanel.SetActive(false);
            quizPanel.SetActive(true);
            quizResultPanel.SetActive(false);

            curQIndex = 0;
            endQuizB.SetActive(true);
            showResultB.SetActive(false);


            var loadedQuestions = Resources.LoadAll<QuizSO>("Data/Quizzes/").Where(x => x.topic.name == selectedTopic.name && x.difficulty == selectedDifficulty).ToList();
            currentQuestions.Clear();
            quizEnd = false;

            foreach (var item in loadedQuestions)
            {
                QuestionData qData = new()
                {
                    questionHeader = item.question,
                    answer = item.options[(int)item.answer].option,
                    options = new()
                };

                List<QuizOptionData> optionsCopy = new(item.options);

                var shuffledOptions = new List<string>();

                for (int i = 0; i < 4; i++)
                {
                    int index = Random.Range(0, optionsCopy.Count);
                    shuffledOptions.Add(optionsCopy[index].option);
                    optionsCopy.RemoveAt(index);
                }

                foreach (var option in shuffledOptions)
                    qData.options.Add(option);

                currentQuestions.Add(qData);
            }

            RefreshQuizUI();
        }

        private void RefreshQuizUI()
        {
            var data = currentQuestions[curQIndex];
            questionLabel.text = $"Question {curQIndex + 1}\n {data.questionHeader}";

            string answer = data.answer;

            for (int i = 0; i < data.options.Count; i++)
            {
                string o = data.options[i];
                OptionsButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = o;
                OptionsButtons[i].interactable = true;
                OptionsButtons[i].GetComponent<Image>().color = Color.white;

                if (quizEnd)
                {
                    OptionsButtons[i].interactable = false;

                    if (string.IsNullOrEmpty(data.selectedOption))
                    {
                        if (o == data.answer)
                            OptionsButtons[i].GetComponent<Image>().color = Color.yellow;
                    }
                    else
                    {
                        if (data.selectedOption == answer)
                        {
                            if (o == answer)
                                OptionsButtons[i].GetComponent<Image>().color = Color.green;
                        }
                        else
                        {
                            if (o == data.selectedOption)
                                OptionsButtons[i].GetComponent<Image>().color = Color.red;
                            if (o == data.answer)
                                OptionsButtons[i].GetComponent<Image>().color = Color.green;
                        }
                    }
                }
                else
                {
                    OptionsButtons[i].interactable = true;

                    if (data.selectedOption == o)
                        OptionsButtons[i].interactable = false;

                    OptionsButtons[i].onClick.RemoveAllListeners();
                    OptionsButtons[i].onClick.AddListener(() => ChooseOption(o));
                }

            }
        }

        List<Button> OptionsButtons => new() { optionA, optionB, optionC, optionD };


        private void ChooseOption(string option)
        {
            currentQuestions[curQIndex].selectedOption = option;
            RefreshQuizUI();
        }

        public void EndQuiz()
        {
            if (quizEnd)
                return;

            endQuizB.SetActive(false);
            showResultB.SetActive(true);

            quizEnd = true;
            RefreshQuizUI();
        }

        public void OpenTopics()
        {
            _createdTopics.ForEach(x => Destroy(x));
            _createdTopics.Clear();

            foreach (var item in DataManager.LoadTopics())
            {
                GameObject g = Instantiate(topicItemPrefab, topicItemPrefab.transform.parent);
                g.GetComponent<UITopicItem>().Init(item);

                switch (item.name)
                {
                    case "topic 02":
                        g.GetComponent<Button>().interactable = PlayerPrefs.GetInt(SaveKeys.TOPIC_2_EASY, 0) == 1;
                        break;
                    case "topic 03":
                        g.GetComponent<Button>().interactable = PlayerPrefs.GetInt(SaveKeys.TOPIC_3_EASY, 0) == 1;
                        break;
                }

                g.GetComponent<Button>().onClick.AddListener(() => OpenDifficulties(item));
                g.SetActive(true);
                _createdTopics.Add(g);
            }

            topicsPanel.SetActive(true);
            difficultyPanel.SetActive(false);
            quizPanel.SetActive(false);
            quizResultPanel.SetActive(false);
        }

        public void OpenQuizResult()
        {
            topicsPanel.SetActive(false);
            difficultyPanel.SetActive(false);
            quizPanel.SetActive(false);
            quizResultPanel.SetActive(true);

            int correct = 0;
            int wrong = 0;
            int skipped = 0;

            foreach (var item in currentQuestions)
            {
                if (string.IsNullOrEmpty(item.selectedOption))
                    skipped++;
                else if (item.selectedOption == item.answer)
                    correct++;
                else
                    wrong++;
            }

            if (correct >= 7)
            {

                switch (selectedTopic.name)
                {
                    case "topic 01":
                        switch (selectedDifficulty)
                        {
                            case Difficulty.Easy:
                                PlayerPrefs.SetInt(SaveKeys.TOPIC_1_INTERMEDIATE, 1);
                                break;
                            case Difficulty.Intermediate:
                                PlayerPrefs.SetInt(SaveKeys.TOPIC_1_ADVANCED, 1);
                                break;
                            case Difficulty.Advanced:
                                PlayerPrefs.SetInt(SaveKeys.TOPIC_2_EASY, 1);
                                break;
                        }
                        break;
                    case "topic 02":
                        switch (selectedDifficulty)
                        {
                            case Difficulty.Easy:
                                PlayerPrefs.SetInt(SaveKeys.TOPIC_2_INTERMEDIATE, 1);
                                break;
                            case Difficulty.Intermediate:
                                PlayerPrefs.SetInt(SaveKeys.TOPIC_2_ADVANCED, 1);
                                break;
                            case Difficulty.Advanced:
                                PlayerPrefs.SetInt(SaveKeys.TOPIC_3_EASY, 1);
                                break;
                        }
                        break;
                    case "topic 03":
                        switch (selectedDifficulty)
                        {
                            case Difficulty.Easy:
                                PlayerPrefs.SetInt(SaveKeys.TOPIC_3_INTERMEDIATE, 1);
                                break;
                            case Difficulty.Intermediate:
                                PlayerPrefs.SetInt(SaveKeys.TOPIC_3_ADVANCED, 1);
                                break;
                            case Difficulty.Advanced:
                                break;
                        }
                        break;
                }
            }

            resultLabel.text = $"Correct:{correct}\nWrong:{wrong}\nSkipped:{skipped}";
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void NextQuestion()
        {
            if (curQIndex == currentQuestions.Count - 1)
                curQIndex = 0;
            else
                curQIndex++;

            RefreshQuizUI();
        }

        public void PreviousQuestion()
        {
            if (curQIndex == 0)
                curQIndex = currentQuestions.Count - 1;
            else
                curQIndex--;

            RefreshQuizUI();
        }
    }
}
