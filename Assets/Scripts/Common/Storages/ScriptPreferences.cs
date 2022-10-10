using System;
using System.Collections;
using System.Collections.Generic;
using Common.Interpreters;
using Common.Keys;
using Common.Storages.Preferences;

namespace Common.Storages
{
    public class ScriptPreferences : IEnumerable<KeyValuePair<string, Value>>, IDisposable
    {
        private readonly Dictionary<string, Value> _variables;

        public ScriptPreferences()
        {
            _variables = Preference.Files.Get(StorageKey.ScriptPreferences, new Dictionary<string, Value>());
        }

        public IEnumerator<KeyValuePair<string, Value>> GetEnumerator()
        {
            return _variables.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Set(string key, Value newValue)
        {
            if (!_variables.ContainsKey(key))
            {
                _variables.Add(key, newValue);
            }
            else
            {
                _variables[key] = newValue;
            }
        }

        public void Dispose()
        {
            Preference.Files.Set(StorageKey.ScriptPreferences, _variables);
            Preference.Files.Save();
        }
    }
}