using Common.Configs;
using Common.Storages.Preferences;
using UnityEngine;
using UnityEngine.Events;

namespace Models.Unit
{
    public class PlayerUnit : BaseUnit
    {
        [SerializeField]
        private UnitConfigObject playerConfig;
        [SerializeField]
        private UnityEvent onReset;

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

        public void ResetState()
        {
            Health = UnitConfig.health;
            onReset.Invoke();
        }
    }
}