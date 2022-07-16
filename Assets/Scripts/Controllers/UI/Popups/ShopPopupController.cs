using System.Collections.Generic;
using Controllers.Items;
using Models.Items;
using Models.PopupSystem;
using UnityEngine;
using Views.UI.Game.Items;

namespace Controllers.UI.Popups
{
    public class ShopPopupController : BasePopupController
    {
        private List<ShopItemController> _itemViews;
        private PlayerInventoryController _playerInventory;

        [SerializeField]
        private Transform itemsTransform;
        [SerializeField]
        private ItemView shopItemControllerPrefab;
        [SerializeField]
        private ItemObject[] items;

        public void Init(in PlayerInventoryController playerInventory)
        {
            _playerInventory = playerInventory;

            _itemViews = new List<ShopItemController>();
            foreach (var itemObject in items)
            {
                var item = Instantiate(shopItemControllerPrefab, itemsTransform);
                var itemController = new ShopItemController();

                item.Init(itemObject, itemController);
                itemController.OnClick += ItemClick;

                _itemViews.Add(itemController);
            }
        }

        private void ItemClick(in ItemObject item)
        {
            _playerInventory.BuyItem(item);
        }

        private void OnDestroy()
        {
            foreach (var itemController in _itemViews)
            {
                itemController.OnClick -= ItemClick;
            }
        }
    }
}