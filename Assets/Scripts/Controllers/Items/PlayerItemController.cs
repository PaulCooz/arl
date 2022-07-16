using Models.Items;

namespace Controllers.Items
{
    public class PlayerItemController : IItemController
    {
        private readonly int _id;
        private readonly IPlayerItemsManager _playerItemsManager;

        private ItemObject _data;

        public PlayerItemController(in int id, in IPlayerItemsManager playerItemsManager)
        {
            _id = id;
            _playerItemsManager = playerItemsManager;
        }

        public void Init(in ItemObject data)
        {
            _data = data;
        }

        public void OnItemClick()
        {
            _playerItemsManager.Use(_id, _data);
        }
    }
}