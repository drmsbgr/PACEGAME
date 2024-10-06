using UnityEngine;

namespace QuantumQuasars.ScriptableObjects
{
    [CreateAssetMenu(fileName = "topic",menuName ="QuantumQuasars/Topic")]
    public class TopicSO : ScriptableObject
    {
        [TextArea(3, 5)] public string topicHeader;
        [TextArea(5, 7)] public string topicDesc;
    }
}
