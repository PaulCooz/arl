using SettingObjects.Keys;
using UnityEngine;

namespace Models.Items
{
    public abstract class BaseGameItem : MonoBehaviour
    {
        [SerializeField]
        protected ItemObject itemObject;

        [SerializeField]
        protected SpriteRenderer spriteRenderer;

        protected virtual void Awake()
        {
            spriteRenderer.sprite = itemObject.ItemSprite;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(Tag.Player) || other.CompareTag(Tag.PlayerGunTrigger)) return;

            Destroy(gameObject);
        }
    }
}