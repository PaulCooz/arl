using System.Collections.Generic;
using Abstracts.Models.Unit;
using Common.Arrays;
using Realisations.Models.CollisionTriggers;
using UnityEngine;

namespace Realisations.Models.Guns
{
    public class Gun : MonoBehaviour, ISerializationCallbackReceiver
    {
        [SerializeField]
        private Bullet bulletPrefab;
        [SerializeField]
        private BaseUnit ownUnit;
        [SerializeField]
        private GunCollisionTrigger unitCollisionTrigger;
        [SerializeField]
        private Transform gunBarrel;
        [SerializeField]
        private float attackRadius;
        [SerializeField]
        private float delay;

        private void OnEnable()
        {
            StartCoroutine(ShootInvoking());
        }

        private IEnumerator<WaitForSeconds> ShootInvoking()
        {
            while (isActiveAndEnabled)
            {
                yield return new WaitForSeconds(delay);

                ShootToNearest();
            }
        }

        private void ShootToNearest()
        {
            var enemies = unitCollisionTrigger.CollidersInRange;
            if (enemies.IsEmpty()) return;

            var enemy = (BaseUnit) null;
            var minDist = attackRadius;

            foreach (var target in enemies)
            {
                var distance = Distance(target);
                var position = gunBarrel.position;
                var hit = Physics2D.Raycast(
                    position,
                    target.transform.position - position,
                    attackRadius
                );

                if (minDist > distance && hit.transform is not null && hit.collider.gameObject == target.gameObject)
                {
                    minDist = distance;
                    enemy = target;
                }
            }

            if (enemy is not null) ShootTo(enemy);
        }

        private void ShootTo(in BaseUnit enemy)
        {
            var direction = (enemy.Position - ownUnit.Position).normalized;
            var bullet = Instantiate(
                bulletPrefab,
                gunBarrel.position,
                Quaternion.Euler(0, 0, Mathf.Sign(direction.y) * Vector2.Angle(direction, Vector2.right))
            );

            bullet.Push(direction);
        }

        private float Distance(in BaseUnit target)
        {
            return Vector2.Distance(target.Position, ownUnit.Position);
        }

#if UNITY_EDITOR
        public void OnBeforeSerialize()
        {
            if (unitCollisionTrigger == null || unitCollisionTrigger.Collider == null) return;
            unitCollisionTrigger.Collider.radius = attackRadius;
        }

        public void OnAfterDeserialize() { }
#endif
    }
}