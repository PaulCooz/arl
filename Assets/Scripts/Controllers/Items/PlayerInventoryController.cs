using Controllers.UI.Popups;
using Controllers.Units.Player;
using Models;
using Models.Items;
using Models.PopupSystem;
using SettingObjects.Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers.Items
{
    public class PlayerInventoryController : MonoBehaviour
    {
        private const int StartCoins = 10;

        private int _coins;
        private PlayerInventory _playerInventory;

        [SerializeField]
        private PopupsController popupsController;
        [SerializeField]
        private PlayerUnitController playerUnitController;

        [SerializeField]
        private UnityEvent<int> onCoinsCountChange;

        public int Coins
        {
            get => _coins;
            private set
            {
                _coins = value;
                onCoinsCountChange?.Invoke(_coins);
            }
        }

        private void Start()
        {
            _playerInventory = new PlayerInventory();
            Coins = StartCoins;

            InputEvents.OnInventoryOpen += ShowInventory;
        }

        public void ShowInventory()
        {
            if (popupsController.IsShowingPopup) return;

            var popup = popupsController.AddPopup<InventoryPopupController>(PopupNames.PlayerInventory);
            popup.Init(_playerInventory, playerUnitController);
        }

        public void PickUpCoins(int delta)
        {
            Coins += delta;
        }

        public void BuyItem(in ItemObject item)
        {
            if (Coins < item.ItemCost) return;
            Coins -= item.ItemCost;

            _playerInventory.Items.Add(item);
        }
    }
}