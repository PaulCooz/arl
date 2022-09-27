using Newtonsoft.Json.Linq;

namespace Common.Storages.Configs
{
    public class Config
    {
        private JObject _config;

        public void Set(string name, object value)
        {
            _config.Add(name, JToken.FromObject(value));
        }

        public T Get<T>(string name, T defaultValue = default)
        {
            return _config.ContainsKey(name) && _config[name] != null ? _config[name].ToObject<T>() : defaultValue;
        }

        public void Save()
        {
            Preferences.Preference.FileProvider.Save();
        }
    }
}