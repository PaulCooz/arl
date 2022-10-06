using System;
using Common;
using Common.Keys;
using Common.Storages.Configs;
using Models.Unit;
using UnityEngine;

namespace Views.Guns.Bullets
{
    public class BulletView : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public void Setup(BaseUnit ownUnit)
        {
            var color = Config.Get(ownUnit.Name, ConfigKey.BulletColor, Array.Empty<byte>()).ToColor();

            spriteRenderer.color = color;
        }
    }
}