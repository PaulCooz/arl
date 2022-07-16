using System.Collections.Generic;
using Libs;
using UnityEngine;
using UnityEngine.UI;

namespace Models.PopupSystem
{
    public class PopupsController : MonoBehaviour
    {
        private readonly Stack<BasePopupController> _popups = new();

        [SerializeField]
        private PopupPrefabs popupPrefabs;
        [SerializeField]
        private RectTransform popupsInstantiateTransform;
        [SerializeField]
        private RaycastBlockerController raycastBlocker;
        [SerializeField]
        private GraphicRaycaster[] othersRaycaster;

        public bool IsShowingPopup => !_popups.IsEmpty();

        private void Start()
        {
            InputEvents.OnBack += RemoveTop;
        }

        public T GetTop<T>() where T : BasePopupController
        {
            return IsShowingPopup ? _popups.Peek() as T : null;
        }

        public void RemoveTop()
        {
            if (!IsShowingPopup) return;

            Destroy(_popups.Pop().gameObject);

            if (IsShowingPopup)
            {
                raycastBlocker.Reshow();
                _popups.Peek().transform.SetAsLastSibling();
            }
            else
            {
                foreach (var raycaster in othersRaycaster)
                {
                    raycaster.enabled = true;
                }

                raycastBlocker.Hide();
            }
        }

        public T AddPopup<T>(string prefabName) where T : BasePopupController
        {
            if (!IsShowingPopup)
            {
                foreach (var raycaster in othersRaycaster)
                {
                    raycaster.enabled = false;
                }

                raycastBlocker.Show();
            }
            else
            {
                raycastBlocker.Reshow();
            }

            var popupController = Instantiate(popupPrefabs[prefabName], popupsInstantiateTransform);
            _popups.Push(popupController);
            popupController.popupsController = this;

            return popupController as T;
        }

        private void OnDestroy()
        {
            InputEvents.OnBack -= RemoveTop;
        }
    }
}
