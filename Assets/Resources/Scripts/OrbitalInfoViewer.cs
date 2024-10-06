using System;
using TMPro;
using UnityEngine;

namespace QuantumQuasars
{
    public class OrbitalInfoViewer : MonoBehaviour
    {
        public static OrbitalInfoViewer instance = null;
        const float cooldown = 2f;
        private float timer = 0f;
        [SerializeField] private TextMeshProUGUI label;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(instance);
                timer = cooldown;
            }
            else if (instance != this)
                Destroy(gameObject);
        }

        private void Update()
        {
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    LoadInfo();
                    timer = cooldown;
                }
            }
        }

        private async void LoadInfo()
        {
            var data = await OrbitInfoAPI.GetOrbitInfo();

            DateTime launchDate = new(2024, 2, 8, 9, 33, 0);

            TimeSpan span = DateTime.Now - launchDate;

            label.text = $"{span.Days}d, {span.Hours}h, {span.Minutes}m\nLAT:{data.latitude}, LON:{data.longitude}";
        }
    }
}
