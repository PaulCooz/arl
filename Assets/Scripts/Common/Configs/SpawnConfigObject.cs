using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Configs
{
    [CreateAssetMenu]
    public class SpawnConfigObject : ScriptableObject
    {
        [Serializable]
        public struct UnitData
        {
            public UnitConfigObject unit;
            public int chance;
        }

        private static SpawnConfigObject _instance;

        public static SpawnConfigObject Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<SpawnConfigObject>("Configs/SpawnConfig");
                }

                return _instance;
            }
        }

        [SerializeField]
        public List<UnitData> units;
    }
}