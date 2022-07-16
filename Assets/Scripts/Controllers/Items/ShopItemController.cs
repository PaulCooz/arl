using Models.Items;

namespace Controllers.Items
{
    public class ShopItemController : IItemController
    {
        private ItemObject _data;

        public delegate void OnBuyItem(in ItemObject item);

        public event OnBuyItem OnClick;

        public void Init(in ItemObject data)
        {
            _data = data;
        }

        public void OnItemClick()
        {
            OnClick?.Invoke(_data);
        }
    }
}