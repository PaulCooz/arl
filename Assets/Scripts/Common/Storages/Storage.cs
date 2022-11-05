using System.IO;
using UnityEngine;

namespace Common.Storages
{
    public static class Storage
    {
        public static readonly string PrefsFilePath = Path.Combine(Application.persistentDataPath, "preferences.json");

        public static bool HasPrefsFile => File.Exists(PrefsFilePath);

        public static void CheckDirectory(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }
    }
}