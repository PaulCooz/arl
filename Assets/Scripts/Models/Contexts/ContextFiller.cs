using System.Collections.Generic;
using Common;
using Common.Audios;
using Common.Interpreters;
using Common.Interpreters.Expressions;
using Common.Keys;
using Models.Unit;
using Models.Weapons;
using UnityEngine;
using UnityEngine.Events;
using Views;

namespace Models.Contexts
{
    public class ContextFiller : MonoBehaviour
    {
        [SerializeField]
        private IntervalTrigger intervalTriggerPrefab;
        [SerializeField]
        private SpriteEffect boomEffect;
        [SerializeField]
        private PlayerUnit playerUnit;

        [SerializeField]
        private UnityEvent<int> addExperience;
        [SerializeField]
        private UnityEvent<int, Vector3> spawnItem;

        private void Awake()
        {
            Context.SetGlobalFunction(ContextKey.SpawnIntervalTrigger, SpawnIntervalFire);
            Context.SetGlobalFunction(ContextKey.AddExperience, AddExperience);
            Context.SetGlobalFunction(ContextKey.GetPlayerHp, GetPlayerHp);
            Context.SetGlobalFunction(ContextKey.SetPlayerHp, SetPlayerHp);
            Context.SetGlobalFunction(ContextKey.BoomEffect, BoomEffect);
            Context.SetGlobalFunction(ContextKey.SpawnItem, SpawnItem);
            Context.SetGlobalFunction(ContextKey.Chance, Chance);
            Context.SetGlobalFunction(ContextKey.PlaySound, PlaySound);
        }

        private Expression PlaySound(in IReadOnlyList<Expression> expressions)
        {
            var values = expressions.ToValues();

            var id = values[0].StringValue;
            var volume = values.Count > 1 ? values[1].DoubleValue : 1;
            var position = values.Count > 2 ? (Vector2) values[2].Vector3Value : playerUnit.Position;

            var sound = SoundMaster.Instance.PlayOnce(id, (float) volume);
            sound.transform.position = position;

            return Expression.Empty;
        }

        private Expression Chance(in IReadOnlyList<Expression> expressions)
        {
            var value = expressions[0].ToValue().IntValue;
            var rand = Random.Range(0, 100);
            return new BooleanExpression(rand < value);
        }

        private Expression SpawnItem(in IReadOnlyList<Expression> expressions)
        {
            spawnItem.Invoke(expressions[0].ToValue().IntValue, expressions[1].ToValue().Vector3Value);
            return Expression.Empty;
        }

        private Expression BoomEffect(in IReadOnlyList<Expression> expressions)
        {
            var position = expressions[0].ToValue().Vector3Value;
            Instantiate(boomEffect, position, Quaternion.identity);
            return Expression.Empty;
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