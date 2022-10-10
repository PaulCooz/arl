namespace Common.Storages.Preferences
{
    public interface IPrefsProvider
    {
        public void Set<T>(string key, T value);
        public T Get<T>(string key, T defaultValue);

        public void SetInt(string key, int value);
        public int GetInt(string key, int defaultValue = default);

        public void SetFloat(string key, float value);
        public float GetFloat(string key, float defaultValue = default);

        public void SetBool(string key, bool value);
        public bool GetBool(string key, bool defaultValue = default);

        public void SetString(string key, string value);
        public string GetString(string key, string defaultValue = default);

        public void SetDouble(string key, double value);
        public double GetDouble(string key, double defaultValue = default);

        public bool HasKey(string key);
        public void DeleteKey(string key);
        public void DeleteAll();
        public void Save();
    }
}