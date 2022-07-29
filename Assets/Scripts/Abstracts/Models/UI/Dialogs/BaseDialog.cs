using UnityEngine;
using Views.Abstracts.UI.Dialogs;

namespace Models.Abstracts.UI.Dialogs
{
    public abstract class BaseDialog : MonoBehaviour
    {
        [SerializeField]
        private BaseDialogView baseDialogView;
    }
}