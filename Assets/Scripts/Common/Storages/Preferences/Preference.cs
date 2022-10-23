namespace Common.Storages.Preferences
{
    public class Preference
    {
        public static readonly IPrefsProvider Registry = new RegistryPrefsProvider();
        public static readonly IPrefsProvider Files = new FilePrefsProvider();
        public static readonly IPrefsProvider Temporary = new TemporaryPrefsProvider();

        public static int Points
        {
            get => Files.GetInt("points", 0);
            set => Files.SetInt("points", value);
        }

        public static int AimPoints
        {
            get => Files.GetInt("aim_points", 5);
            set => Files.SetInt("aim_points", value);
        }

        public static bool Sound
        {
            get => Registry.GetBool("sound", true);
            set => Registry.SetBool("sound", value);
        }

        public static bool ServiceSingIn
        {
            get => Registry.GetBool("service_sing_in", false);
            set => Registry.SetBool("service_sing_in", value);
        }

        public static int CurrentLevel
        {
            get => Registry.GetInt("current_level", 0);
            set => Registry.SetInt("current_level", value);
        }

        public static int PlayerHealth
        {
            get => Files.GetInt("player_health", -1);
            set => Files.SetInt("player_health", value);
        }
    }
}