using Models.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.UI.Game.Items
{
    public class ItemView : MonoBehaviour
    {
        private IItemController _itemController;
        private ItemObject _data;

        [SerializeField]
        protected Image image;
        [SerializeField]
        protected TMP_Text itemName;
        [SerializeField]
        protected Button button;

        public void Init(in ItemObject itemObject, in IItemController itemController)
        {
            _data = itemObject;
            image.sprite = _data.ItemSprite;
            itemName.text = _data.ItemName;
            _itemController = itemController;

            button.onClick.AddListener(_itemController.OnItemClick);

            _itemController.Init(_data);
        }
    }
}