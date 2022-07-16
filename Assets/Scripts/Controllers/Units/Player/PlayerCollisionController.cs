using Controllers.Items;
using Controllers.Items.GameItems;
using SettingObjects.Keys;
using UnityEngine;

namespace Controllers.Units.Player
{
    public class PlayerCollisionController : MonoBehaviour
    {
        [SerializeField]
        private PlayerEventController playerEventController;
        [SerializeField]
        private Collider2D thisCollider;

        private void OnTriggerEnter2D(Collider2D collider2d)
        {
            if (collider2d.CompareTag(Tag.MapExit) && collider2d.IsTouching(thisCollider))
            {
                playerEventController.MapDone();
            }

            if (collider2d.TryGetComponent<CoinItemController>(out var coin))
            {
                playerEventController.PickUpCoins(coin.CoinsCount);
            }
        }
    }
}