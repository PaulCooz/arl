using System.Collections.Generic;
using Models.Abstracts.Guns;
using UnityEngine;

namespace Models.Abstracts.Units.ShootingUnits
{
    public abstract class BaseShootingUnit : BaseUnit
    {
        private readonly List<BaseShootingUnit> _unitsInRange = new();

        [SerializeField]
        protected BaseGun gun;

        private void OnTriggerEnter2D(Collider2D collider2d)
        {
            if (!collider2d.TryGetComponent<BaseShootingUnit>(out var unit)) return;

            _unitsInRange.Add(unit);
        }

        private void OnTriggerExit2D(Collider2D collider2d)
        {
            if (!collider2d.TryGetComponent<BaseShootingUnit>(out var unit)) return;

            _unitsInRange.Remove(unit);
        }

        public void AttackNearest()
        {
            var nearest = (BaseShootingUnit) null;
            var minDist = float.MaxValue;
            foreach (var unit in _unitsInRange)
            {
                var distance = Vector2.Distance(transform.position, unit.transform.position);
                if (minDist <= distance) continue;

                minDist = distance;
                nearest = unit;
            }

            if (nearest == null) return;

            gun.AttackTo(nearest);
        }

        public void TakeDamage(in DamageData damageData)
        {
        }
    }
}