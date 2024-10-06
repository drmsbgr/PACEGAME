using QuantumQuasars.ScriptableObjects;
using System;
using TMPro;
using UnityEngine;

namespace QuantumQuasars
{
    public class UITopicItem : MonoBehaviour
    {
        private TopicSO data;
        [SerializeField] private TextMeshProUGUI headerLabel;

        public void Init(TopicSO data)
        {
            this.data = data;
            Refresh();
        }

        private void Refresh()
        {
            headerLabel.text = data.topicHeader;
        }
    }
}
