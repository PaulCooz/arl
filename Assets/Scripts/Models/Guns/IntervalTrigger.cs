using System.Collections.Generic;
using Common;
using Common.Interpreters;
using Common.Keys;
using Common.Storages.Configs;
using Models.CollisionTriggers;
using UnityEngine;

namespace Models.Guns
{
    public class IntervalTrigger : MonoBehaviour
    {
        private WaitForSeconds _waitForSeconds;
        private int _count;
        private string _config;

        [SerializeField]
        private BoxCollider2D boxCollider;
        [SerializeField]
        private UnitCollisionTrigger unitTrigger;

        public void Setup(in string config)
        {
            _config = config;
            _waitForSeconds = new WaitForSeconds(Config.Get(_config, ConfigKey.Interval, 1f));

            _count = Config.Get(_config, ConfigKey.Count, 3);
            unitTrigger.isTriggerEnemy = Config.Get(_config, ConfigKey.IsEnemyTrigger, true);
            boxCollider.size = Config.Get(_config, ConfigKey.ColliderSize, new[] {1f, 1f}).ToVector2();
            boxCollider.offset = Config.Get(_config, ConfigKey.ColliderOffset, new[] {0f, 0f}).ToVector2();
            boxCollider.edgeRadius = Config.Get(_config, ConfigKey.ColliderEdgeRadius, 0f);

            StartCoroutine(Fire());
        }

        private IEnumerator<WaitForSeconds> Fire()
        {
            for (var count = 0; count < _count; count++)
            {
                yield return _waitForSeconds;

                AttackInRange();
            }
        }

        private void AttackInRange()
        {
            var script = new Script();
            script.SetVariable(ContextKey.Damage, Config.Get(_config, ConfigKey.Damage, 1).ToScriptValue());

            foreach (var unit in unitTrigger.CollidersInRange)
            {
                script.SetVariable(ContextKey.EnemyName, unit.Name.ToScriptValue());
                script.SetProperty
                (
                    ContextKey.EnemyHp,
                    () => unit.Health.ToScriptValue(),
                    value => unit.Health = value.IntValue
                );

                script.Run(Config.Get(_config, ConfigKey.OnTrigger, ""));
            }
        }
    }
}