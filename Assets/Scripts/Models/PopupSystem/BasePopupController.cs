using System;
using UnityEngine;

namespace Models.PopupSystem
{
    public abstract class BasePopupController : MonoBehaviour
    {
        [NonSerialized]
        public PopupsController popupsController;

        public void Close()
        {
            popupsController.RemoveTop();
        }
    }
}
