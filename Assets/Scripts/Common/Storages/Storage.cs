using System.IO;
using UnityEngine;

namespace Common.Storages
{
    public static class Storage
    {
        public static readonly string Root = Application.persistentDataPath;

        public static void CheckDirectory(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }
    }
}