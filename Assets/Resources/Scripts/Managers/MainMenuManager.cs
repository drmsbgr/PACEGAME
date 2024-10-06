using QuantumQuasars.Keys;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuantumQuasars.Manager
{
    public class MainMenuManager : MonoBehaviour
    {
        private GameObject currentPanel;
        [SerializeField] private GameObject mainMenu, settings;

        private void Start()
        {
            if (!PlayerPrefs.HasKey(SaveKeys.TOPIC_1_INTERMEDIATE))
                PlayerPrefs.SetInt(SaveKeys.TOPIC_1_INTERMEDIATE, 0);

            if (!PlayerPrefs.HasKey(SaveKeys.TOPIC_1_ADVANCED))
                PlayerPrefs.SetInt(SaveKeys.TOPIC_1_ADVANCED, 0);

            if (!PlayerPrefs.HasKey(SaveKeys.TOPIC_2_EASY))
                PlayerPrefs.SetInt(SaveKeys.TOPIC_2_EASY, 0);

            if (!PlayerPrefs.HasKey(SaveKeys.TOPIC_2_INTERMEDIATE))
                PlayerPrefs.SetInt(SaveKeys.TOPIC_2_INTERMEDIATE, 0);

            if (!PlayerPrefs.HasKey(SaveKeys.TOPIC_2_ADVANCED))
                PlayerPrefs.SetInt(SaveKeys.TOPIC_2_ADVANCED, 0);

            if (!PlayerPrefs.HasKey(SaveKeys.TOPIC_3_EASY))
                PlayerPrefs.SetInt(SaveKeys.TOPIC_3_EASY, 0);

            if (!PlayerPrefs.HasKey(SaveKeys.TOPIC_3_INTERMEDIATE))
                PlayerPrefs.SetInt(SaveKeys.TOPIC_3_INTERMEDIATE, 0);

            if (!PlayerPrefs.HasKey(SaveKeys.TOPIC_3_ADVANCED))
                PlayerPrefs.SetInt(SaveKeys.TOPIC_3_ADVANCED, 0);

            OpenPanel(mainMenu);
        }

        public void OpenQuiz()
        {
            SceneManager.LoadScene("Quiz");
        }

        public void OpenCards()
        {
            SceneManager.LoadScene("Cards");
        }

        public void OpenMedia()
        {
            SceneManager.LoadScene("Media");
        }

        public void OpenSettings()
        {
            OpenPanel(settings);
        }

        public void Back()
        {
            OpenPanel(mainMenu);
        }

        public void Quit()
        {
            Application.Quit();
        }

        private void OpenPanel(GameObject next)
        {
            if (currentPanel != null)
                currentPanel.SetActive(false);
            currentPanel = next;
            currentPanel.SetActive(true);
        }
    }
}
