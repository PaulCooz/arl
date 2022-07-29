using System;

namespace Abstracts.Models.Storage
{
    public interface ISaveManager
    {
        int GetInt(string key, int defaultValue);
        bool GetBool(string key, bool defaultValue);
        float GetFloat(string key, float defaultValue);
        string GetString(string key, string defaultValue);
        double GetDouble(string key, double defaultValue);
        decimal GetDecimal(string key, decimal defaultValue);
        DateTime GetDateTime(string key, DateTime defaultValue);
        T Get<T>(string key, T defaultValue);

        void SetInt(string key, int value);
        void SetBool(string key, bool value);
        void SetFloat(string key, float value);
        void SetString(string key, string value);
        void SetDouble(string key, double value);
        void SetDecimal(string key, decimal value);
        void SetDateTime(string key, DateTime value);
        void Set<T>(string key, T value);

        bool HasKey(string key);
        void DeleteKey(string key);
        void Save();
        void DeleteAll();
    }
}