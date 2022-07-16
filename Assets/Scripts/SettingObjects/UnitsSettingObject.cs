using System;
using SettingObjects.Keys;
using UnityEngine;

namespace SettingObjects
{
    [Serializable]
    public class UnitsSetting
    {
        [Serializable]
        public class SlimeSetting
        {
            public byte dropItemChance;
        }

        public SlimeSetting slimeSetting;
    }

    [CreateAssetMenu(fileName = ResourcesKeys.UnitsSetting, menuName = "ScriptableObjects/Units Setting")]
    public class UnitsSettingObject : ScriptableObject
    {
        [SerializeField]
        public UnitsSetting unitsSetting;
    }
}
