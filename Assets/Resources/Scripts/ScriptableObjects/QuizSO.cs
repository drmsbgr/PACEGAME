using System.Collections.Generic;
using UnityEngine;
using QuantumQuasars.Enums;

namespace QuantumQuasars.ScriptableObjects
{
    [CreateAssetMenu(fileName = "quiz", menuName = "QuantumQuasars/Quiz")]
    public partial class QuizSO : ScriptableObject
    {
        [TextArea(3, 5)]
        public string question;
        public List<QuizOptionData> options;
        public Options answer;
        public Difficulty difficulty;
        public TopicSO topic;
    }

    [System.Serializable]
    public class QuizOptionData
    {
        public string option;
    }
}
