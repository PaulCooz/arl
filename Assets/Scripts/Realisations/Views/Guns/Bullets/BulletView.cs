using System;
using Common;
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
            var color = ConvertHelper.ArrayToColor
            (
                Config.Get
                (
                    unitName,
                    ConfigKey.BulletColor,
                    Array.Empty<byte>()
                )
            );

            spriteRenderer.color = color;
        }
    }
}