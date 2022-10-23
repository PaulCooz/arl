using System.Collections.Generic;
using UnityEngine;

namespace Common.Storages.Preferences
{
    public class TemporaryPrefsProvider : IPrefsProvider
    {
        private readonly Dictionary<string, string> _data;

        public TemporaryPrefsProvider()
        {
            _data = new Dictionary<string, string>();
        }

        public void Set<T>(string key, T value)
        {
            _data[key] = JsonUtility.ToJson(value);
        }

        public T Get<T>(string key, T defaultValue)
        {
            return HasKey(key) ? JsonUtility.FromJson<T>(_data[key]) : defaultValue;
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

        public bool HasKey(string key) => _data.ContainsKey(key);

        public void DeleteKey(string key) => _data.Remove(key);

        public void DeleteAll() => _data.Clear();

        public void Save() { }
    }
}