namespace Common.Storages.Preferences.PrefsContainers
{
    public class SettingPrefs : Prefs
    {
        public SettingPrefs(in IPrefsProvider provider) : base(in provider) { }

        public bool Sound
        {
            get => provider.GetBool("sound", true);
            set => provider.SetBool("sound", value);
        }

        public bool ServiceSingIn
        {
            get => provider.GetBool("service_sing_in", false);
            set => provider.SetBool("service_sing_in", value);
        }
    }
}