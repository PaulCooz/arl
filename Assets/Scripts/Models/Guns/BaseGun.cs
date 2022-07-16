using System.Collections.Generic;
using Libs;
using Models.Units;
using UnityEngine;

namespace Models.Guns
{
    public abstract class BaseGun : MonoBehaviour
    {
        protected readonly List<BaseUnit> enemies = new();

        [SerializeField]
        protected BaseUnit ownUnit;
        [SerializeField]
        protected Transform gunBarrel;

        [SerializeField]
        protected float attackRadius;

        protected void ShootToNearestEnemy()
        {
            if (enemies.IsEmpty()) return;

            var enemy = (BaseUnit) null;
            var minDist = attackRadius;

            foreach (var target in enemies)
            {
                var distance = Distance(target);
                var position = gunBarrel.position;
                var hit = Physics2D.Raycast
                (
                    position,
                    target.transform.position - position,
                    attackRadius
                );

                if (minDist > distance && hit.transform is not null && hit.transform.gameObject == target.gameObject)
                {
                    minDist = distance;
                    enemy = target;
                }
            }

            if (enemy is not null) ShootTo(enemy);
        }

        protected void ShootToAllEnemies()
        {
            if (enemies.IsEmpty()) return;

            foreach (var target in enemies)
            {
                if (!InAttackField(target)) continue;

                ShootTo(target);
            }
        }

        private bool InAttackField(in BaseUnit target)
        {
            return Distance(target) <= attackRadius;
        }

        private float Distance(in BaseUnit target)
        {
            return Vector2.Distance(target.OwnRigidbody.position, ownUnit.OwnRigidbody.position);
        }

        protected abstract void ShootTo(BaseUnit unit);
    }
}
