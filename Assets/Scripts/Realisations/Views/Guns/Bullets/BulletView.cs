using Common.Keys;
using Common.Storages.Configs;
using UnityEngine;

namespace Realisations.Views.Guns.Bullets
{
    public class BulletView : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public void Setup(string unitName)
        {
            spriteRenderer.color = new Color32(Config.Get<byte>(unitName, ConfigKey.BulletColor, 255), 10, 10, 255);
        }
    }
}