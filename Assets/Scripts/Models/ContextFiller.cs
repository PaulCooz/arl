using System.Collections.Generic;
using Common.Interpreters;
using Common.Keys;
using Models.Guns;
using UnityEngine;

namespace Models
{
    public class ContextFiller : MonoBehaviour
    {
        [SerializeField]
        private IntervalTrigger intervalTriggerPrefab;

        private void Awake()
        {
            Context.SetGlobalFunction(ContextKey.SpawnIntervalTrigger, SpawnIntervalFire);
        }

        private Expression SpawnIntervalFire(in IReadOnlyList<Expression> expressions)
        {
            var values = expressions.ToValues();
            var fire = Instantiate(intervalTriggerPrefab, values[1].Vector3Value, Quaternion.identity);
            fire.Setup(values[0].StringValue);

            return Expression.Empty;
        }
    }
}