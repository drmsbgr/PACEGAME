using QuantumQuasars.ScriptableObjects;
using QuantumQuasars.UIItem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace QuantumQuasars.Manager
{
    public class CardsManager : MonoBehaviour
    {
        [SerializeField] private GameObject cardItemPrefab;
        [SerializeField] private GameObject topicItemPrefab;
        [SerializeField] private GameObject cardPanel;
        [SerializeField] private GameObject topicPanel;
        private readonly List<GameObject> _createdCards = new();

        private void Start()
        {
            foreach (var item in DataManager.LoadTopics())
            {
                GameObject g = Instantiate(topicItemPrefab, topicItemPrefab.transform.parent);
                g.GetComponent<UITopicItem>().Init(item);
                g.GetComponent<Button>().onClick.AddListener(() => OpenCards(item.name));
                g.SetActive(true);
            }
        }

        private void OpenCards(string key)
        {
            topicPanel.SetActive(false);
            cardPanel.SetActive(true);
            LoadCardsByTopic(key);
        }

        private void LoadCardsByTopic(string topicName)
        {
            _createdCards.ForEach(x => Destroy(x));
            _createdCards.Clear();
            var cards = Resources.LoadAll<CardSO>("Data");
            foreach (var card in cards)
            {
                if (card.topic.name != topicName)
                    continue;

                GameObject g = Instantiate(cardItemPrefab, cardItemPrefab.transform.parent);
                g.GetComponent<UICardItem>().Init(card);
                g.SetActive(true);
                _createdCards.Add(g);
            }
        }

        public void SelectTopic()
        {
            topicPanel.SetActive(true);
            cardPanel.SetActive(false);
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
