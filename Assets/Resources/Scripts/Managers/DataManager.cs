using QuantumQuasars.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QuantumQuasars.Manager
{
    public static class DataManager
    {
        public static List<TopicSO> LoadTopics()
        {
            return Resources.LoadAll<TopicSO>("Data/Topics/").ToList();
        }
    }
}
