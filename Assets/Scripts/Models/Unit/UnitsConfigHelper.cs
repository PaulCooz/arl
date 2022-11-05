using System;
using System.Collections.Generic;
using System.Linq;
using Common.Configs;
using UnityEngine;

namespace Models.Unit
{
    public class UnitsConfigHelper : MonoBehaviour
    {
        private List<SpawnConfigObject.UnitData> _units;

        public void BeforeFirstLevel()
        {
            _units = SpawnConfigObject.Instance.units;
        }

        public UnitConfigObject GetUnitConfig(in System.Random random)
        {
            var maxChance = _units.Sum(unit => unit.chance);
            var chance = random.Next(0, maxChance + 1);
            var current = 0;
            foreach (var unit in _units)
            {
                current += unit.chance;
                if (current < chance) continue;

                return unit.unit;
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}