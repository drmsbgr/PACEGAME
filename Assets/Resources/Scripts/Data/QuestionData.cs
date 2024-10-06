using QuantumQuasars.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace QuantumQuasars.Data
{
    [System.Serializable]
    public class QuestionData
    {
        public string questionHeader;
        public List<string> options;
        public string answer;
        public string selectedOption;
    }
}
