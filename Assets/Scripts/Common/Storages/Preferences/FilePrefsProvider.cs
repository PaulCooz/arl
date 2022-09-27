using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common.Storages.Preferences
{
    public class FilePrefsProvider : IPrefsProvider
    {
        private static readonly string PrefsFilePath = Path.Combine(Storage.Root, "preferences.json");

        private readonly JObject _prefs;

        public FilePrefsProvider()
        {
            _prefs = File.Exists(PrefsFilePath) ? JObject.Parse(File.ReadAllText(PrefsFilePath)) : new JObject();
        }

        public void Set<T>(string key, T value)
        {
            if (HasKey(key))
            {
                _prefs[key] = JToken.FromObject(value);
            }
            else
            {
                _prefs.Add(key, JToken.FromObject(value));
            }
        }

        public T Get<T>(string key, T defaultValue)
        {
            return HasKey(key) && _prefs[key] != null ? _prefs[key].ToObject<T>() : defaultValue;
        }

        public void SetInt(string key, int value) => Set(key, value);
        public int GetInt(string key, int defaultValue = default) => Get(key, defaultValue);
        public void SetFloat(string key, float value) => Set(key, value);

        public float GetFloat(string key, float defaultValue = default) => Get(key, defaultValue);
        public void SetBool(string key, bool value) => Set(key, value);
        public bool GetBool(string key, bool defaultValue = default) => Get(key, defaultValue);
        public void SetString(string key, string value) => Set(key, value);
        public string GetString(string key, string defaultValue = default) => Get(key, defaultValue);
        public void SetDouble(string key, double value) => Set(key, value);
        public double GetDouble(string key, double defaultValue = default) => Get(key, defaultValue);

        public bool HasKey(string key)
        {
            return _prefs.ContainsKey(key);
        }

        public void DeleteKey(string key)
        {
            _prefs.Remove(key);
        }

        public void DeleteAll()
        {
            File.Delete(Storage.Root);
            _prefs.RemoveAll();
        }

        public void Save()
        {
            File.WriteAllText(Storage.Root, _prefs.ToString(Formatting.None));
        }
    }
}