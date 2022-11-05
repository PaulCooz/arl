using Common.Configs;
using Common.Keys;
using Common.Storages.Preferences;

namespace Models.Unit
{
    public class PlayerUnit : BaseUnit
    {
        protected override void PreInitAndSetHealth()
        {
            UnitConfig = Config.GetUnit(ConfigKey.Player);

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