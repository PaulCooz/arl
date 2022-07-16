using System.Collections.Generic;

namespace Models.Items
{
    public class PlayerInventory
    {
        public List<ItemObject> Items { get; }

        public PlayerInventory()
        {
            Items = new List<ItemObject>();
        }
    }
}