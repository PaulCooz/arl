using System;
using Libs;
using SettingObjects.Keys;
using UnityEngine;

namespace SettingObjects
{
    [Serializable]
    public class MapSettings
    {
        [Serializable]
        public class RoomFiller
        {
            public byte enemyChance;
            public byte shopChance;
            public byte exitChance;
        }

        [Serializable]
        public class MapMaker
        {
            public MinMaxInt startRoomWidth;
            public MinMaxInt startRoomHeight;

            public MinMaxInt roomWidth;
            public MinMaxInt roomHeight;

            public ushort clearDeadEndMaxIterations;
        }

        public RoomFiller roomFiller;
        public MapMaker mapMaker;
    }

    [CreateAssetMenu(fileName = ResourcesKeys.MapSetting, menuName = "ScriptableObjects/Map Setting")]
    public class MapSettingObject : ScriptableObject
    {
        [SerializeField]
        public MapSettings mapSettings;
    }
}
