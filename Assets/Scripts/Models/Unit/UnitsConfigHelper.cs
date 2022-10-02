using System;
using System.Collections.Generic;
using System.Linq;
using Common.Keys;
using Common.Storages.Configs;
using Newtonsoft.Json;
using UnityEngine;

namespace Models.Unit
{
    public class UnitsConfigHelper : MonoBehaviour
    {
        private struct UnitData
        {
            [JsonProperty("name")]
            public string Name;
            [JsonProperty("chance")]
            public int Chance;
        }

        private List<UnitData> _units;

        public void BeforeFirstLevel()
        {
            _units = Config.Get<List<UnitData>>(ConfigKey.SpawnableObjects, ConfigKey.Units);
        }

        public string GetUnitName(in System.Random random)
        {
            var maxChance = _units.Sum(unit => unit.Chance);
            var chance = random.Next(0, maxChance + 1);
            var current = 0;
            foreach (var unit in _units)
            {
                current += unit.Chance;
                if (current < chance) continue;

                return unit.Name;
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}