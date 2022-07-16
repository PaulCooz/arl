using UnityEngine;
using UnityEngine.Events;

namespace Controllers.Units.Player
{
    public class PlayerEventController : MonoBehaviour
    {
        public UnityEvent onMapDone;
        public UnityEvent onPlayerDie;
        public UnityEvent<int> onPlayerPickUpCoins;

        public void MapDone()
        {
            onMapDone?.Invoke();
        }

        public void PickUpCoins(in int delta)
        {
            onPlayerPickUpCoins?.Invoke(delta);
        }

        public void PlayerDie()
        {
            onPlayerDie?.Invoke();
        }
    }
}
