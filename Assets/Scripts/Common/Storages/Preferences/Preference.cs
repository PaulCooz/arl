namespace Common.Storages.Preferences
{
    public class Preference
    {
        public static readonly IPrefsProvider RegistryProvider = new RegistryPrefsProvider();
        public static readonly IPrefsProvider FileProvider = new FilePrefsProvider();
    }
}