using System.Collections.Generic;
using Common.Arrays;
using Common.Keys;
using Common.Storages.Configs;
using Models.CollisionTriggers;
using Models.Unit;
using UnityEngine;
using UnityEngine.Events;

namespace Models.Guns
{
    public class Gun : MonoBehaviour
    {
        private BaseUnit _ownUnit;

        [SerializeField]
        private Bullet bulletPrefab;
        [SerializeField]
        private BaseUnit ownUnit;
        [SerializeField]
        private GunCollisionTrigger unitCollisionTrigger;
        [SerializeField]
        private Transform gunBarrel;
        [SerializeField]
        private float delay;
        [SerializeField]
        private bool isFromPlayer;

        [SerializeField]
        private UnityEvent<float> reloadStatus;

        public BaseUnit OwnUnit
        {
            get => _ownUnit;
            set
            {
                _ownUnit = value;
                unitCollisionTrigger.Collider.radius = AttackRadius;
            }
        }

        public float AttackRadius => Config.Get(OwnUnit.Name, ConfigKey.AttackRadius, 4f);

        private void OnEnable()
        {
            StartCoroutine(ShootInvoking());
        }

        private IEnumerator<WaitForSeconds> ShootInvoking()
        {
            yield return new WaitForSeconds(Random.Range(0f, delay));

            while (isActiveAndEnabled)
            {
                var time = 0f;
                while (time < delay)
                {
                    time += Time.deltaTime;
                    reloadStatus.Invoke(1f - time / delay);

                    yield return null;
                }

                ShootToNearest();
            }
        }

        private void ShootToNearest()
        {
            var enemies = unitCollisionTrigger.CollidersInRange;
            if (enemies.IsEmpty()) return;

            var enemy = (BaseUnit) null;
            var minDist = AttackRadius;

            foreach (var target in enemies)
            {
                var distance = Distance(target);
                var position = gunBarrel.position;
                var hit = Physics2D.Raycast
                (
                    position,
                    target.transform.position - position,
                    AttackRadius
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
            var bullet = Instantiate
            (
                bulletPrefab,
                gunBarrel.position,
                Quaternion.Euler(0, 0, Mathf.Sign(direction.y) * Vector2.Angle(direction, Vector2.right))
            );

            bullet.Setup(OwnUnit);
            bullet.Push(direction, isFromPlayer);
        }

        private float Distance(in BaseUnit target)
        {
            return Vector2.Distance(target.Position, ownUnit.Position);
        }
    }
}