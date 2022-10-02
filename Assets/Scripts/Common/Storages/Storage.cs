using System.IO;
using Common.Storages.Configs;
using UnityEngine;

namespace Common.Storages
{
    public static class Storage
    {
        public static readonly string Root = Application.persistentDataPath;

        public static readonly string PrefsFilePath = Path.Combine(Root, "preferences.json");

        public static string ConfigFilesRootPath
        {
            get
            {
                var directory = Path.Combine(Root, "configs");
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                    Directory.CreateDirectory(Path.Combine(directory, "units"));
                    DefaultConfigs.Create(directory);
                }

                return directory;
            }
        }

        public static void CheckDirectory(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }
    }
}