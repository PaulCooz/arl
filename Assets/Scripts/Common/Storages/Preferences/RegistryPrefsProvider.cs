using System.Globalization;
using UnityEngine;

namespace Common.Storages.Preferences
{
    public class RegistryPrefsProvider : IPrefsProvider
    {
        public void Set<T>(string key, T value)
        {
            PlayerPrefs.SetString(key, JsonUtility.ToJson(value));
        }

        public T Get<T>(string key, T defaultValue)
        {
            return JsonUtility.FromJson<T>(PlayerPrefs.GetString(key, JsonUtility.ToJson(defaultValue)));
        }

        public void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public int GetInt(string key, int defaultValue = default)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        public float GetFloat(string key, float defaultValue = default)
        {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }

        public void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        public bool GetBool(string key, bool defaultValue = default)
        {
            return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) != 0;
        }

        public void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public string GetString(string key, string defaultValue = default)
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }

        public void SetDouble(string key, double value)
        {
            PlayerPrefs.SetString(key, value.ToString(CultureInfo.InvariantCulture));
        }

        public double GetDouble(string key, double defaultValue = default)
        {
            return double.Parse
            (
                PlayerPrefs.GetString(key, defaultValue.ToString(CultureInfo.InvariantCulture)),
                CultureInfo.InvariantCulture
            );
        }

        public bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        public void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        public void Save()
        {
            PlayerPrefs.Save();
        }
    }
}