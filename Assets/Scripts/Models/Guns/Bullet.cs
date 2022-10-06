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
        private float force;

        [SerializeField]
        private UnityEvent<BaseUnit> onSetup;

        public void Setup(BaseUnit ownUnit)
        {
            _ownUnit = ownUnit;
            onSetup.Invoke(_ownUnit);
        }

        private void OnTriggerEnter2D(Collider2D collider2d)
        {
            if (!collider2d.CompareTag(Tags.Wall)) return;

            Destroy(gameObject);
        }

        public void Push(in Vector2 direction, in bool isFromPlayer)
        {
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
            script.SetVariable("own_name", _ownUnit.Name.ToScriptValue());
            script.SetVariable("enemy_name", unit.Name.ToScriptValue());

            script.SetVariable("own_damage", Config.Get(_ownUnit.Name, ConfigKey.Damage, 1).ToScriptValue());
            script.SetVariable("enemy_damage", Config.Get(unit.Name, ConfigKey.Damage, 1).ToScriptValue());

            script.SetProperty
            (
                "own_hp",
                () => _ownUnit.Health.ToScriptValue(),
                value => _ownUnit.Health = value.IntValue
            );
            script.SetProperty
            (
                "enemy_hp",
                () => unit.Health.ToScriptValue(),
                value => unit.Health = value.IntValue
            );
        }
    }
}