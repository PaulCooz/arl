using UnityEngine;
using UnityEngine.UI;

namespace Realisations.Views
{
    public class UiInputView : MonoBehaviour
    {
        private static readonly int Offset = Shader.PropertyToID("_Offset");

        [SerializeField]
        private Image image;

        private void Awake()
        {
            image.material = Instantiate(image.material);
        }

        public void UpdatePosition(Vector2 pos)
        {
            image.material.SetVector(Offset, pos);
        }
    }
}