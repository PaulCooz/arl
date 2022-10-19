using System;
using System.Collections.Generic;
using System.Linq;
using Common.Interpreters;
using Common.Storages;
using UnityEngine;

namespace Models.Contexts
{
    public class ContextVariables : MonoBehaviour
    {
        private ScriptPreferences _preferences;

        [SerializeField]
        private ContextValuesObject contextValues;

        private void OnEnable()
        {
            _preferences = new ScriptPreferences(contextValues.Values);

            foreach (var pair in _preferences)
            {
                Context.SetGlobalVariable(pair.Key, pair.Value);
            }
        }

        public void UpdateValue(in string key, Value newValue)
        {
            _preferences.Set(key, newValue);
            Context.SetGlobalVariable(key, newValue);
        }

        private void OnDisable()
        {
            _preferences.Dispose();
            _preferences = null;
        }
    }
}