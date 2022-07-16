using Controllers.Units.Player;
using Models.Items;
using UnityEngine;

namespace Controllers.Items.GameItems
{
    public class CoinItemController : BaseGameItem
    {
        private PlayerEventController _playerEvent;

        [SerializeField]
        private int coinsToAdd;

        public int CoinsCount => coinsToAdd;

        public void SetEvents(in PlayerEventController playerEvent)
        {
            _playerEvent = playerEvent;

            _playerEvent.onMapDone.AddListener(MapDone);
        }

        private void MapDone()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _playerEvent.onMapDone.RemoveListener(MapDone);
        }
    }
}
