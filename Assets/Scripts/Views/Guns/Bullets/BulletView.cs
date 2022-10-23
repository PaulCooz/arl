using System;
using Common;
using Common.Keys;
using Common.Storages.Configs;
using UnityEngine;

namespace Views.Guns.Bullets
{
    public class BulletView : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public void Setup(string ownUnitName)
        {
            var bulletConfig = Config.Get(ownUnitName, ConfigKey.BulletConfig, ConfigKey.BaseBullet);
            var color = Config.Get(bulletConfig, ConfigKey.Color, Array.Empty<byte>()).ToColor();

            spriteRenderer.color = color;
        }
    }
}