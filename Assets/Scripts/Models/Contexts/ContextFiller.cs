using System.Collections.Generic;
using Common.Interpreters;
using Common.Interpreters.Expressions;
using Common.Keys;
using Models.Guns;
using Models.Unit;
using UnityEngine;
using UnityEngine.Events;

namespace Models.Contexts
{
    public class ContextFiller : MonoBehaviour
    {
        [SerializeField]
        private IntervalTrigger intervalTriggerPrefab;
        [SerializeField]
        private PlayerUnit playerUnit;

        [SerializeField]
        private UnityEvent<int> addExperience;

        private void Awake()
        {
            Context.SetGlobalFunction(ContextKey.SpawnIntervalTrigger, SpawnIntervalFire);
            Context.SetGlobalFunction(ContextKey.AddExperience, AddExperience);
            Context.SetGlobalFunction(ContextKey.GetPlayerHp, GetPlayerHp);
            Context.SetGlobalFunction(ContextKey.SetPlayerHp, SetPlayerHp);
        }

        private Expression GetPlayerHp(in IReadOnlyList<Expression> expressions)
        {
            return playerUnit.Health.ToScriptExpression();
        }

        private Expression SetPlayerHp(in IReadOnlyList<Expression> expressions)
        {
            playerUnit.Health = expressions[0].ToValue().IntValue;

            return Expression.Empty;
        }

        private Expression AddExperience(in IReadOnlyList<Expression> expressions)
        {
            addExperience.Invoke(expressions[0].ToValue().IntValue);

            return Expression.Empty;
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