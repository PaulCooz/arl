using System.IO;
using Common.Storages.Configs;
using UnityEngine;

namespace Common.Storages
{
    public static class Storage
    {
        public static readonly string Root =
#if UNITY_EDITOR
            Path.Combine("Assets", "Resources");
#else
            Application.persistentDataPath;
#endif

        public static readonly string PrefsFilePath = Path.Combine(Application.persistentDataPath, "preferences.json");

        public static bool HasPrefsFile => File.Exists(PrefsFilePath);
        public static string ConfigFilesRootPath
        {
            get
            {
                var directory = Path.Combine(Root, "configs");
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                    Directory.CreateDirectory(Path.Combine(directory, "units"));
                    DefaultConfigs.Create(Root);
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