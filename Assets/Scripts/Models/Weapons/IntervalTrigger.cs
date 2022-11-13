using System.Collections.Generic;
using Common.Configs;
using Common.Interpreters;
using Common.Keys;
using Models.CollisionTriggers;
using UnityEngine;

namespace Models.Weapons
{
    public class IntervalTrigger : MonoBehaviour
    {
        private WaitForSeconds _waitForSeconds;
        private int _count;
        private IntervalTriggerConfigObject _config;

        [SerializeField]
        private BoxCollider2D boxCollider;
        [SerializeField]
        private UnitCollisionTrigger unitTrigger;

        public void Setup(in string config)
        {
            _config = ConfigContainer.GetTrigger(config);
            _waitForSeconds = new WaitForSeconds(_config.interval);

            _count = _config.count;
            unitTrigger.isTriggerEnemy = _config.isEnemyTrigger;
            boxCollider.size = _config.colliderSize;
            boxCollider.offset = _config.colliderOffset;
            boxCollider.edgeRadius = _config.colliderEdgeRadius;

            StartCoroutine(Fire());
        }

        private IEnumerator<WaitForSeconds> Fire()
        {
            for (var count = 0; count < _count; count++)
            {
                yield return _waitForSeconds;

                AttackInRange();
            }

            Destroy(gameObject);
        }

        private void AttackInRange()
        {
            var script = new Script();
            script.SetVariable(ContextKey.Damage, _config.damage.ToScriptValue());

            var units = unitTrigger.CollidersInRange.ToArray();
            for (var i = 0; i < units.Length; i++)
            {
                var unit = units[i];

                script.SetProperty
                (
                    ContextKey.EnemyHp,
                    () => unit.Health.ToScriptValue(),
                    value => unit.Health = value.IntValue
                );

                script.Run(_config.onTrigger);
            }
        }
    }
}