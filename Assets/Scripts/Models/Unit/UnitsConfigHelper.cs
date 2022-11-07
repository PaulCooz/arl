using System;
using System.Linq;
using Common.Configs;
using UnityEngine;

namespace Models.Unit
{
    public class UnitsConfigHelper : MonoBehaviour
    {
        private UnitConfigObject[] _units;

        public void BeforeFirstLevel()
        {
            _units = Resources.LoadAll<UnitConfigObject>("Configs/Units");
        }

        public UnitConfigObject GetUnitConfig(in System.Random random)
        {
            var maxChance = _units.Sum(unit => unit.spawnChance);
            var chance = random.Next(0, maxChance + 1);
            var current = 0;
            foreach (var unit in _units)
            {
                current += unit.spawnChance;
                if (current < chance) continue;

                return unit;
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}