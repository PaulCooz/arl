namespace Common.Storages.Preferences
{
    public class Preference
    {
        public static readonly IPrefsProvider Registry = new RegistryPrefsProvider();
        public static readonly IPrefsProvider Files = new FilePrefsProvider();
    }
}