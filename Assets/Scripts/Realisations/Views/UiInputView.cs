using UnityEngine;
using UnityEngine.UI;

namespace Realisations.Views
{
    public class UiInputView : MonoBehaviour
    {
        private const float LengthScale = 0.1f;

        private static readonly int Offset = Shader.PropertyToID("_Offset");
        private static readonly int Direction = Shader.PropertyToID("_Direction");

        private Vector2 _downPos;

        [SerializeField]
        private Image image;

        private void Awake()
        {
            image.material = Instantiate(image.material);
        }

        public void SetPosition(Vector2 pos)
        {
            _downPos = pos;
            image.material.SetVector(Offset, pos);
        }

        public void UpdateDirection(Vector2 pos, float strength)
        {
            image.material.SetVector(Direction, _downPos - LengthScale * pos * strength);
        }
    }
}