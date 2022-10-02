namespace Common.Keys
{
    public class ResourceKey
    {
        private const string SpawnableObjects = "spawnable_objects";
        private const string BaseUnit = "units/base_unit";
        private const string CommonEnemy = "units/common_enemy";
        private const string Player = "units/player";
        private const string StaticEnemy = "units/static_enemy";

        public static readonly string[] Configs =
        {
            SpawnableObjects,
            BaseUnit,
            CommonEnemy,
            Player,
            StaticEnemy
        };
    }
}