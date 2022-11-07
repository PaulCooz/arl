using Common.Configs;
using Common.Storages.Preferences;
using UnityEngine;

namespace Models.Unit
{
    public class PlayerUnit : BaseUnit
    {
        [SerializeField]
        private UnitConfigObject playerConfig;

        protected override void PreInitAndSetHealth()
        {
            UnitConfig = playerConfig;

            var hp = Preference.Game.PlayerHealth;
            Health = hp <= 0 ? UnitConfig.health : hp;

            onHealthChange.AddListener(UpdatePrefs);
        }

        private void UpdatePrefs(int newHealth)
        {
            Preference.Game.PlayerHealth = newHealth;
        }
    }
}