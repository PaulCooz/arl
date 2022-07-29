using System.Collections.Generic;
using Models.Abstracts.Guns;
using UnityEngine;
using UnityEngine.Events;

namespace Models.Abstracts.Units.ShootingUnits
{
    public abstract class BaseShootingUnit : BaseUnit
    {
        private readonly List<BaseShootingUnit> _unitsInRange = new();

        [SerializeField]
        protected BaseGun gun;

        protected event UnityAction<BaseShootingUnit> OnUnitAdd;
        protected event UnityAction<BaseShootingUnit> OnUnitRemove;

        private void AddUnit(BaseShootingUnit unit)
        {
            _unitsInRange.Add(unit);
            OnUnitAdd?.Invoke(unit);
        }

        private void RemoveUnit(BaseShootingUnit unit)
        {
            _unitsInRange.Remove(unit);
            OnUnitRemove?.Invoke(unit);
        }

        private void OnTriggerEnter2D(Collider2D collider2d)
        {
            if (!collider2d.TryGetComponent<BaseShootingUnit>(out var unit)) return;

            AddUnit(unit);
        }

        private void OnTriggerExit2D(Collider2D collider2d)
        {
            if (!collider2d.TryGetComponent<BaseShootingUnit>(out var unit)) return;

            RemoveUnit(unit);
        }

        protected void AttackNearest()
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
    }
}