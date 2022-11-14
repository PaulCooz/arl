using System;
using Common.Configs;
using Models.Maps.Abstracts;
using UnityEngine;

namespace Models.Progressions
{
    [CreateAssetMenu]
    public class LevelProgressionObject : ScriptableObject
    {
        [Serializable]
        private struct Data
        {
            public UnitConfigObject[] units;
            public MapData map;
            public bool isBossFight;
        }

        [SerializeField]
        private Data[] levels;
        [SerializeField]
        private Data infinity;

        public void GetData(in int level, out MapData map, out UnitConfigObject[] units, out bool isBossFight)
        {
            var data = level < levels.Length ? levels[level] : infinity;

            map = data.map;
            map.seed = level;

            units = data.units;

            isBossFight = data.isBossFight;
        }
    }
}