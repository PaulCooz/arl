using System;
using Models.Items.Potions;
using Models.Units;
using SettingObjects.Keys;

namespace Models.Items
{
    public class ItemsFactory
    {
        public IPotion GetPotion(in string potionName, in BaseUnit player)
        {
            var potion = potionName switch
            {
                PotionNames.HealthPotion => new HealthPotion(),
                _ => throw new ArgumentOutOfRangeException(nameof(potionName), potionName, "miss potion")
            };
            potion.SetPlayer(player);

            return potion;
        }
    }
}