using UnityEngine;

namespace QuantumQuasars.ScriptableObjects
{
    [CreateAssetMenu(fileName = "card_", menuName = "QuantumQuasars/Card")]
    public class CardSO : ScriptableObject
    {
        [TextArea(3, 5)] public string question;
        [TextArea(8, 12)] public string information;
        public TopicSO topic;
    }
}
