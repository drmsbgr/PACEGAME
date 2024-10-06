using QuantumQuasars.Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace QuantumQuasars
{
    public class UIMediaItem : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image imageSource;

        public void Init(Sprite sprite)
        {
            this.imageSource.sprite = sprite;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                MediaManager.instance.imageViewPanel.SetActive(true);
                MediaManager.instance.imageSource.sprite = imageSource.sprite;
            }
        }
    }
}
