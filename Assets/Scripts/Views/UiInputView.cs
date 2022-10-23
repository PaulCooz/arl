using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class UiInputView : MonoBehaviour
    {
        private Vector2 _downPos;
        private Material _material;

        [SerializeField]
        private Image image;
        [SerializeField]
        private RectTransform rectTransform;

        public bool IsShowing
        {
            get => image.enabled;
            set => image.enabled = value;
        }

        public void SetPosition(Vector2 pos)
        {
            _downPos = pos;
            rectTransform.anchoredPosition = pos;
        }
    }
}