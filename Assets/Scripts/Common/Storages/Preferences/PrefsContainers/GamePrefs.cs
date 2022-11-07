namespace Common.Storages.Preferences.PrefsContainers
{
    public class GamePrefs : Prefs
    {
        private const string CurrentLevelKey = "current_level";
        private const string PointsKey = "points";
        private const string AimPointsKey = "aim_points";
        private const string PlayerHealthKey = "player_health";

        private static readonly string[] AllKeys = {CurrentLevelKey, PointsKey, AimPointsKey, PlayerHealthKey};

        public GamePrefs(in IPrefsProvider provider) : base(in provider) { }

        public int CurrentLevel
        {
            get => provider.GetInt(CurrentLevelKey, 0);
            set => provider.SetInt(CurrentLevelKey, value);
        }

        public int Points
        {
            get => provider.GetInt(PointsKey, 0);
            set => provider.SetInt(PointsKey, value);
        }

        public int AimPoints
        {
            get => provider.GetInt(AimPointsKey, 5);
            set => provider.SetInt(AimPointsKey, value);
        }

        public int PlayerHealth
        {
            get => provider.GetInt(PlayerHealthKey, -1);
            set => provider.SetInt(PlayerHealthKey, value);
        }

        public void ToDefault()
        {
            foreach (var key in AllKeys)
            {
                provider.DeleteKey(key);
            }
        }
    }
}