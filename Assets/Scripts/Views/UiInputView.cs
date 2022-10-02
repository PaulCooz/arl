using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class UiInputView : MonoBehaviour
    {
        private const float LengthScale = 0.1f;

        private static readonly int Offset = Shader.PropertyToID("_Offset");
        private static readonly int Direction = Shader.PropertyToID("_Direction");

        private Vector2 _downPos;
        private Material _material;

        [SerializeField]
        private Image image;

        public bool IsShowing
        {
            get => image.enabled;
            set => image.enabled = value;
        }

        private void Awake()
        {
            _material = Instantiate(image.material);
            image.material = _material;
        }

        public void SetPosition(Vector2 pos)
        {
            _downPos = pos;
            _material.SetVector(Offset, pos);
        }

        public void UpdateDirection(Vector2 pos, float strength)
        {
            _material.SetVector(Direction, _downPos - LengthScale * pos * strength);
        }
    }
}