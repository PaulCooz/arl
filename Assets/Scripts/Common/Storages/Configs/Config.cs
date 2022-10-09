using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common.Storages.Configs
{
    public class Config
    {
        private const string Parent = "parent";

        private static readonly Dictionary<string, JObject> GlobalConfig;

        static Config()
        {
            GlobalConfig = new Dictionary<string, JObject>();

            var configs = Directory.GetFiles(Storage.ConfigFilesRootPath, "*.json", SearchOption.AllDirectories);
            foreach (var path in configs)
            {
                GlobalConfig.Add
                (
                    Path.GetFileNameWithoutExtension(path),
                    JObject.Parse(File.ReadAllText(path))
                );
            }
        }

        public static void Set(string config, string name, object value)
        {
            GlobalConfig[config].Add(name, JToken.FromObject(value));
        }

        public static T Get<T>(string config)
        {
            return GlobalConfig[config].ToObject<T>();
        }

        public static T Get<T>(string config, string name, T defaultValue = default)
        {
            var jToken = GetToken(config, name);
            return jToken == null ? defaultValue : jToken.ToObject<T>();
        }

        public static void Save()
        {
            foreach (var (key, value) in GlobalConfig)
            {
                File.WriteAllText
                (
                    Path.Combine(Storage.ConfigFilesRootPath, $"{key}.json"),
                    value.ToString(Formatting.None)
                );
            }
        }

        private static JToken GetToken(string config, string name)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            while (!GlobalConfig[config].ContainsKey(name))
            {
                if (!GlobalConfig[config].ContainsKey(Parent)) return null;

                // ReSharper disable once PossibleNullReferenceException
                config = GlobalConfig[config][Parent].ToObject<string>();
            }

            return GlobalConfig[config][name];
        }
    }
}