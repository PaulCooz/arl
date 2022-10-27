using Common.Storages.Preferences.PrefsContainers;

namespace Common.Storages.Preferences
{
    public class Preference
    {
        public static readonly IPrefsProvider Registry = new RegistryPrefsProvider();
        public static readonly IPrefsProvider Files = new FilePrefsProvider();
        public static readonly IPrefsProvider Temporary = new TemporaryPrefsProvider();

        #region Containers

        public static readonly GamePrefs Game = new(Files);
        public static readonly SettingPrefs Setting = new(Registry);

        #endregion
    }
}