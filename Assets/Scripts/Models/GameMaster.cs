using UnityEngine;
using UnityEngine.Events;

namespace Models
{
    public class GameMaster : MonoBehaviour
    {
        public UnityEvent onLevelCreated;
        public UnityEvent onNextLevel;

        public static event UnityAction OnLevelDone;

        public void NextLevel()
        {
            OnLevelDone?.Invoke();
            onNextLevel.Invoke();
        }

        public void LevelCreated()
        {
            onLevelCreated.Invoke();
        }
    }
}