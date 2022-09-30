using System.IO;
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
                CheckDirectory(directory);
                return directory;
            }
        }

        public static void CheckDirectory(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }
    }
}