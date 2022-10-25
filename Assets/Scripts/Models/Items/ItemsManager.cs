using System;
using UnityEngine;

namespace Models.Items
{
    public class ItemsManager : MonoBehaviour
    {
        [SerializeField]
        private Item itemPrefab;

        [SerializeField]
        private ItemData[] items;

        private ItemData Get(in int id)
        {
            foreach (var item in items)
            {
                if (item.id == id) return item;
            }

            throw new IndexOutOfRangeException();
        }

        public void Spawn(int id, Vector3 position)
        {
            var item = Instantiate(itemPrefab, position, Quaternion.identity);
            item.Setup(Get(id));
        }
    }
}