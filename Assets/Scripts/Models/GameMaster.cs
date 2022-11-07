using UnityEngine;
using UnityEngine.Events;

namespace Models
{
    public class GameMaster : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent doneLevel;
        [SerializeField]
        private UnityEvent clearOldLevel;
        [SerializeField]
        private UnityEvent createdNewLevel;

        public static event UnityAction OnDoneLevel;
        public static event UnityAction OnClearOldLevel;
        public static event UnityAction OnCreatedNewLevel;

        public void DoneLevel()
        {
            doneLevel.Invoke();
            OnDoneLevel?.Invoke();
        }

        public void ClearOldLevel()
        {
            clearOldLevel.Invoke();
            OnClearOldLevel?.Invoke();
        }

        public void CreatedNewLevel()
        {
            createdNewLevel.Invoke();
            OnCreatedNewLevel?.Invoke();
        }
    }
}