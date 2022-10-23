using Common.Keys;
using Common.Storages.Preferences;

namespace Models.Unit
{
    public class PlayerUnit : BaseUnit
    {
        public override void Initialization()
        {
            Name = ConfigKey.Player;
            health = Preference.PlayerHealth;

            base.Initialization();

            onHealthChange.Invoke(health, maxHealth);
            onHealthChange.AddListener(UpdatePrefs);
        }

        private void UpdatePrefs(int newHealth, int _)
        {
            Preference.PlayerHealth = newHealth;
        }
    }
}