using Abstracts.Views.UI.Dialogs;
using UnityEngine;

namespace Abstracts.Models.UI.Dialogs
{
    public abstract class BaseDialog : MonoBehaviour
    {
        [SerializeField]
        private BaseDialogView baseDialogView;
    }
}