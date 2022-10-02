using System.IO;
using UnityEngine;

namespace Common.Storages
{
    public static class Resource
    {
        public static string LoadConfig(string path)
        {
            return Resources.Load<TextAsset>(Path.Combine("configs", path)).text;
        }
    }
}