using UnityEngine;
using UnityEngine.Events;

namespace Realisations.Models
{
    public class GameMaster : MonoBehaviour
    {
        public UnityEvent onNextLevel;

        public void NextLevel()
        {
            onNextLevel.Invoke();
        }
    }
}