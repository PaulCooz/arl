using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Interpreters;
using UnityEngine;

namespace Models.Contexts
{
    [CreateAssetMenu]
    public class ContextValuesObject : ScriptableObject
    {
        [SerializeField]
        private Pair<string, string>[] values;

        public Dictionary<string, Value> Values => values.ToDictionary(p => p.key, p => new Value(p.value));
    }
}