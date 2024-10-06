using QuantumQuasars.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace QuantumQuasars.UIItem
{
    public class UICardItem : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Animator animator;
        private bool flipped;
        [SerializeField] private TextMeshProUGUI frontLabel, backLabel;
        private CardSO data;

        public void Init(CardSO data)
        {
            this.data = data;
            Refresh();
        }

        private void Refresh()
        {
            frontLabel.text = data.question;
            backLabel.text = data.information;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            flipped = !flipped;
            animator.SetBool("flipped", flipped);
        }
    }
}
