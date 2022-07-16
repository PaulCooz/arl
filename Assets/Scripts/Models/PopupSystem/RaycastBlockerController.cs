using UnityEngine;
using UnityEngine.UI;

namespace Models.PopupSystem
{
    public class RaycastBlockerController : MonoBehaviour
    {
        [SerializeField]
        private Image image;

        public void Show()
        {
            Reshow();
            image.enabled = true;
        }

        public void Reshow()
        {
            transform.SetAsLastSibling();
        }

        public void Hide()
        {
            image.enabled = false;
        }
    }
}
