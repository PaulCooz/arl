using Common.Interpreters;
using Common.Keys;
using Common.Storages.Configs;
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
        private UnityEvent<string> onSetup;

        public void Setup(BaseUnit ownUnit)
        {
            _ownUnit = ownUnit;
            onSetup.Invoke(_ownUnit.Name);
        }

        private void OnTriggerEnter2D(Collider2D collider2d)
        {
            if (!collider2d.CompareTag(Tags.Wall)) return;

            Destroy(gameObject);
        }

        public void Push(in Vector2 direction, in bool isFromPlayer)
        {
            var bulletConfig = Config.Get(_ownUnit.Name, ConfigKey.BulletConfig, "base_bullet");
            var force = Config.Get(bulletConfig, ConfigKey.Force, 3f);

            collisionTrigger.isTriggerEnemy = isFromPlayer;
            bulletRigidbody.velocity = direction * force;
        }

        public void Damage(BaseUnit unit)
        {
            var script = new Script();
            FillDamageContext(unit, script);

            var bulletConfig = Config.Get(_ownUnit.Name, ConfigKey.BulletConfig, "base_bullet");
            script.Run(Config.Get(bulletConfig, ConfigKey.OnCollide, ""));

            Destroy(gameObject);
        }

        private void FillDamageContext(BaseUnit unit, Script script)
        {
            var bulletConfig = Config.Get(_ownUnit.Name, ConfigKey.BulletConfig, "base_bullet");

            script.SetVariable(ConfigKey.OwnName, _ownUnit.Name.ToScriptValue());
            script.SetVariable(ConfigKey.EnemyName, unit.Name.ToScriptValue());

            script.SetVariable(ConfigKey.Damage, Config.Get(bulletConfig, ConfigKey.Damage, 1).ToScriptValue());
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