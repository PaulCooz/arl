using Common.Configs;
using UnityEngine;

namespace Views.Guns.Bullets
{
    public class BulletView : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public void Setup(string ownUnitName)
        {
            var bulletConfig = Config.GetUnit(ownUnitName).bulletConfig;
            var color = bulletConfig.color;

            spriteRenderer.color = color;
        }
    }
}