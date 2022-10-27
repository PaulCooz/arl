namespace Common.Storages.Preferences.PrefsContainers
{
    public class GamePrefs : Prefs
    {
        public GamePrefs(in IPrefsProvider provider) : base(in provider) { }

        public int CurrentLevel
        {
            get => provider.GetInt("current_level", 0);
            set => provider.SetInt("current_level", value);
        }

        public int Points
        {
            get => provider.GetInt("points", 0);
            set => provider.SetInt("points", value);
        }

        public int AimPoints
        {
            get => provider.GetInt("aim_points", 5);
            set => provider.SetInt("aim_points", value);
        }

        public int PlayerHealth
        {
            get => provider.GetInt("player_health", -1);
            set => provider.SetInt("player_health", value);
        }
    }
}