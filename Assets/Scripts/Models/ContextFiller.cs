using System.Collections.Generic;
using Common.Interpreters;
using Models.Guns;
using UnityEngine;

namespace Models
{
    public class ContextFiller : MonoBehaviour
    {
        [SerializeField]
        private IntervalFire intervalFirePrefab;

        private void Awake()
        {
            Context.SetGlobalFunction("spawn_interval_fire", SpawnIntervalFire);
        }

        private Expression SpawnIntervalFire(in IReadOnlyList<Expression> expressions)
        {
            var values = expressions.ToValues();
            var fire = Instantiate(intervalFirePrefab, values[1].Vector3Value, Quaternion.identity);
            fire.Setup(values[0].StringValue);

            return Expression.Empty;
        }
    }
}