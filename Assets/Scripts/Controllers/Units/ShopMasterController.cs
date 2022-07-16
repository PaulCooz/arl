using System.Collections;
using Controllers.Game;
using Controllers.Items;
using Controllers.UI.Popups;
using Controllers.Units.Player;
using Models.PopupSystem;
using SettingObjects.Keys;
using UnityEngine;
using Views.Units;

namespace Controllers.Units
{
    public class ShopMasterController : MonoBehaviour
    {
        private WaitForSeconds _waitPlayerCheck;
        private PlayerEventController _playerEvents;
        private PopupsController _popups;
        private Transform _playerTransform;
        private PlayerInventoryController _playerInventory;

        [SerializeField]
        private ShopMasterView masterView;
        [SerializeField]
        private float playerCheckRadius;
        [SerializeField]
        private float playerCheckInterval;

        public void Init(in GameSceneBehaviours sceneBehaviours)
        {
            _waitPlayerCheck = new WaitForSeconds(playerCheckInterval);

            _popups = sceneBehaviours.Popups;
            _playerTransform = sceneBehaviours.PlayerUnit.transform;
            _playerEvents = sceneBehaviours.PlayerUnit.PlayerEvents;
            _playerInventory = sceneBehaviours.PlayerInventory;

            _playerEvents.onMapDone.AddListener(Die);
        }

        private void Start()
        {
            StartCoroutine(CheckForPlayer());
        }

        private IEnumerator CheckForPlayer()
        {
            while (isActiveAndEnabled)
            {
                yield return _waitPlayerCheck;

                if (Vector2.Distance(_playerTransform.position, transform.position) > playerCheckRadius)
                {
                    continue;
                }

                masterView.StartGreetings();
                break;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag(Tag.Player) || _popups.IsShowingPopup) return;

            var shop = _popups.AddPopup<ShopPopupController>(PopupNames.Shop);
            shop.Init(_playerInventory);
        }

        private void Die()
        {
            _playerEvents.onMapDone.RemoveListener(Die);
            Destroy(gameObject);
        }
    }
}
