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
        [SerializeField]
        private UnityEvent gameOver;

        public static event UnityAction OnClearOldLevel;

        public void DoneLevel()
        {
            doneLevel.Invoke();
        }

        public void ClearOldLevel()
        {
            clearOldLevel.Invoke();
            OnClearOldLevel?.Invoke();
        }

        public void CreatedNewLevel()
        {
            createdNewLevel.Invoke();
        }

        public void PlayerDie()
        {
            gameOver.Invoke();
        }
    }
}