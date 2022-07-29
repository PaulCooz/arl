using TMPro;
using UnityEngine;

namespace Abstracts.Views.UI.Dialogs
{
    public abstract class BaseDialogView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text title;

        public string TitleText
        {
            get => title.text;
            set => title.text = value;
        }
    }
}