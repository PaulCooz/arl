using Common.Keys;
using Common.Storages.Configs;
using Common.Storages.Preferences;

namespace Models.Unit
{
    public class PlayerUnit : BaseUnit
    {
        protected override void PreInitAndSetHealth()
        {
            Name = ConfigKey.Player;

            var hp = Preference.PlayerHealth;
            Health = hp <= 0 ? Config.Get(Name, "health", 2) : hp;

            onHealthChange.AddListener(UpdatePrefs);
        }

        private void UpdatePrefs(int newHealth)
        {
            Preference.PlayerHealth = newHealth;
        }
    }
}