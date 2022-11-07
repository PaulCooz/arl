using Common.Configs;
using UnityEngine;

namespace Views.Guns.Bullets
{
    public class BulletView : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public void Setup(UnitConfigObject config)
        {
            spriteRenderer.color = config.bulletConfig.color;
        }
    }
}