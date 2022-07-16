using System.Collections.Generic;
using Controllers.Items;
using Controllers.Units.Player;
using Libs;
using Models.Items;
using Models.PopupSystem;
using UnityEngine;
using Views.UI.Game.Items;

namespace Controllers.UI.Popups
{
    public class InventoryPopupController : BasePopupController, IPlayerItemsManager
    {
        private class ItemWithId : Pair<int, ItemView>
        {
            public int Id => left;
            public ItemView Item => right;

            public ItemWithId(int left, ItemView right) : base(left, right)
            {
            }
        }

        private PlayerUnitController _player;
        private PlayerInventory _playerInventory;

        private List<ItemWithId> _itemViews;

        [SerializeField]
        private ItemView itemViewPrefab;
        [SerializeField]
        private Transform itemsTransform;

        public void Init(in PlayerInventory playerInventory, in PlayerUnitController player)
        {
            _player = player;
            _playerInventory = playerInventory;
            _itemViews = new List<ItemWithId>();

            for (var i = 0; i < _playerInventory.Items.Count; i++)
            {
                var item = Instantiate(itemViewPrefab, itemsTransform);
                var itemController = new PlayerItemController(i, this);

                item.Init(_playerInventory.Items[i], itemController);

                _itemViews.Add(new ItemWithId(i, item));
            }
        }

        public void Use(in int id, in ItemObject itemObject)
        {
            var itemsFactory = new ItemsFactory();

            if (itemObject.ItemType == ItemType.Potion)
            {
                var potion = itemsFactory.GetPotion(itemObject.ItemName, _player);
                potion.Use();

                RemoveItem(id);
            }
        }

        private void RemoveItem(int id)
        {
            var index = _itemViews.FindIndex(item => item.Id == id);

            _playerInventory.Items.RemoveAt(index);

            Destroy(_itemViews[index].Item.gameObject);
            _itemViews.RemoveAt(index);
        }
    }
}