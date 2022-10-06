namespace Common.Keys
{
    public class ResourceKey
    {
        public const string SpawnableObjects = "configs/spawnable_objects";
        public const string BaseUnit = "configs/units/base_unit";
        public const string CommonEnemy = "configs/units/enemy";
        public const string Player = "configs/units/player";
        public const string StaticEnemy = "configs/units/static_enemy";

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