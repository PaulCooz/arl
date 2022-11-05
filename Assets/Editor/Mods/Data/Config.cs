using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Editor.Mods.Data
{
    public class Config : IEnumerable<KeyValuePair<string, JToken>>
    {
        private readonly JObject _config;

        public Config(in string json)
        {
            _config = JObject.Parse(json);
        }

        public int GetInt(string name)
        {
            return _config[name]?.ToObject<int>() ?? throw new NullReferenceException();
        }

        public double GetDouble(string name)
        {
            return _config[name]?.ToObject<double>() ?? throw new NullReferenceException();
        }

        public string GetString(string name)
        {
            return _config[name]?.ToObject<string>();
        }

        public string GetScript(string name)
        {
            return _config[name]?.ToObject<string>() ?? throw new NullReferenceException();
        }

        public IEnumerator<KeyValuePair<string, JToken>> GetEnumerator()
        {
            return new ConfigEnumerator(_config);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class ConfigEnumerator : IEnumerator<KeyValuePair<string, JToken>>
        {
            private readonly IEnumerator<KeyValuePair<string, JToken>> _enumerator;

            public KeyValuePair<string, JToken> Current => _enumerator.Current;

            object IEnumerator.Current => Current;

            public ConfigEnumerator(in JObject main)
            {
                var sum = main;
                var parent = main["parent"]?.ToObject<string>();

                ConfigFiles.Update();
                while (!string.IsNullOrEmpty(parent))
                {
                    var next = JObject.Parse(ConfigFiles.GetJson(parent));

                    parent = next["parent"]?.ToObject<string>();
                    next.Remove("parent");

                    foreach (var p in next)
                    {
                        if (sum.ContainsKey(p.Key)) continue;

                        sum.Add(p.Key, p.Value);
                    }
                }

                _enumerator = sum.GetEnumerator();
            }

            public bool MoveNext() => _enumerator.MoveNext();
            public void Reset() => _enumerator.Reset();
            public void Dispose() => _enumerator.Dispose();
        }
    }
}