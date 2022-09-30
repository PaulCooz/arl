using Abstracts.Models.Unit;
using Common.Interpreters;
using Common.Keys;
using Common.Storages.Configs;
using Realisations.Models.CollisionTriggers;
using UnityEngine;
using UnityEngine.Events;

namespace Realisations.Models.Guns
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
            script.Run("set_enemy_hp(get_enemy_hp() - own_damage)");

            Destroy(gameObject);
        }

        private void FillDamageContext(BaseUnit unit, Script script)
        {
            script.AddVariable("own_name", _ownUnit.Name);
            script.AddVariable("enemy_name", unit.Name);

            script.AddVariable("own_damage", Config.Get(_ownUnit.Name, ConfigKey.Damage, "1"));
            script.AddVariable("enemy_damage", Config.Get(unit.Name, ConfigKey.Damage, "1"));

            script.AddProperty
            (
                "own_hp",
                () => _ownUnit.Health.ToString(),
                s =>
                {
                    Tools.ParseNumber(s, out var count);
                    _ownUnit.Health = count;
                }
            );
            script.AddProperty
            (
                "enemy_hp",
                () => unit.Health.ToString(),
                s =>
                {
                    Tools.ParseNumber(s, out var count);
                    unit.Health = count;
                }
            );
        }
    }
}