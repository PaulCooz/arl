using UnityEngine;
using UnityEngine.Events;

namespace Realisations.Models
{
    public class GameMaster : MonoBehaviour
    {
        public UnityEvent onNextLevel;

        public static event UnityAction OnLevelDone;

        public void NextLevel()
        {
            OnLevelDone?.Invoke();
            onNextLevel.Invoke();
        }
    }
}