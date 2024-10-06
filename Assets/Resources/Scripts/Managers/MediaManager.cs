using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace QuantumQuasars.Manager
{
    public class MediaManager : MonoBehaviour
    {
        public static MediaManager instance = null;
        [SerializeField] private GameObject mediaItemPrefab;
        public GameObject imageViewPanel;
        public Image imageSource;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            var list = Resources.LoadAll<Sprite>("Data/Media/");

            foreach (var item in list)
            {
                GameObject g = Instantiate(mediaItemPrefab, mediaItemPrefab.transform.parent);
                g.GetComponent<UIMediaItem>().Init(item);
                g.SetActive(true);
            }
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
