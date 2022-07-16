using SettingObjects.Keys;
using UnityEngine;

namespace Models.Items
{
    [CreateAssetMenu(fileName = ResourcesKeys.ItemObject, menuName = "ScriptableObjects/Game Item")]
    public class ItemObject : ScriptableObject
    {
        [SerializeField]
        protected Sprite itemSprite;

        [SerializeField]
        protected string itemName;
        [SerializeField]
        protected string itemDescription;
        [SerializeField]
        protected int itemCost;
        [SerializeField]
        protected ItemType itemType;

        public Sprite ItemSprite => itemSprite;

        public string ItemName => itemName;
        public string ItemDescription => itemDescription;
        public int ItemCost => itemCost;
        public ItemType ItemType => itemType;
    }
}