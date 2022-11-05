using System.Collections.Generic;
using System.IO;

namespace Editor.Mods.Data
{
    public static class ConfigFiles
    {
        private static List<string> _paths;

        public static void Update()
        {
            _paths = new List<string>();

            var files = Directory.GetFiles("Assets/Resources/configs", "*.json", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                _paths.Add(file);
            }
        }

        public static string GetJson(in string configName)
        {
            foreach (var path in _paths)
            {
                if (Path.GetFileNameWithoutExtension(path) != configName) continue;

                return File.ReadAllText(path);
            }

            return null;
        }
    }
}