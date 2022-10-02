using System.IO;
using Common.Keys;

namespace Common.Storages.Configs
{
    public static class DefaultConfigs
    {
        public static void Create(string dirPath)
        {
            foreach (var config in ResourceKey.Configs)
            {
                File.WriteAllText($"{Path.Combine(dirPath, config)}.json", Resource.LoadConfig(config));
            }
        }
    }
}