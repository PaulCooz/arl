namespace Common.Storages.Preferences.PrefsContainers
{
    public class Prefs
    {
        protected readonly IPrefsProvider provider;

        protected Prefs(in IPrefsProvider provider)
        {
            this.provider = provider;
        }
    }
}