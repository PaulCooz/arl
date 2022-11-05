using Common.Configs;
using Common.Interpreters;
using Common.Keys;
using Models.CollisionTriggers;
using Models.Unit;
using UnityEngine;
using UnityEngine.Events;

namespace Models.Guns
{
    public class Bullet : MonoBehaviour
    {
        private BaseUnit _ownUnit;

        [SerializeField]
        private UnitCollisionTrigger collisionTrigger;
        [SerializeField]
        private Rigidbody2D bulletRigidbody;

        [SerializeField]
        private UnityEvent<UnitConfigObject> onSetup;

        public void Setup(BaseUnit ownUnit)
        {
            _ownUnit = ownUnit;
            onSetup.Invoke(_ownUnit.UnitConfig);
        }

        private void OnTriggerEnter2D(Collider2D collider2d)
        {
            if (!collider2d.CompareTag(Tags.Wall)) return;

            Destroy(gameObject);
        }

        public void Push(in Vector2 direction, in bool isFromPlayer)
        {
            var bulletConfig = _ownUnit.UnitConfig.bulletConfig;

            collisionTrigger.isTriggerEnemy = isFromPlayer;
            bulletRigidbody.velocity = direction * bulletConfig.force;
        }

        public void Damage(BaseUnit unit)
        {
            var script = new Script();
            FillDamageContext(unit, script);

            var bulletConfig = _ownUnit.UnitConfig.bulletConfig;
            script.Run(bulletConfig.onCollide);

            Destroy(gameObject);
        }

        private void FillDamageContext(BaseUnit unit, Script script)
        {
            var bulletConfig = _ownUnit.UnitConfig.bulletConfig;

            script.SetVariable(ConfigKey.Damage, bulletConfig.damage.ToScriptValue());
            script.SetVariable(ConfigKey.CollidePosition, transform.position.ToScriptValue());

            script.SetProperty
            (
                ConfigKey.OwnHp,
                () => _ownUnit.Health.ToScriptValue(),
                value => _ownUnit.Health = value.IntValue
            );
            script.SetProperty
            (
                ConfigKey.EnemyHp,
                () => unit.Health.ToScriptValue(),
                value => unit.Health = value.IntValue
            );
        }
    }
}