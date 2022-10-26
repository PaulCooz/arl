using System.Collections.Generic;
using Common;
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
        private bool _hasInRange;

        [SerializeField]
        private Bullet bulletPrefab;
        [SerializeField]
        private BaseUnit ownUnit;
        [SerializeField]
        private GunCollisionTrigger unitCollisionTrigger;
        [SerializeField]
        private Transform gunBarrel;
        [SerializeField]
        private bool isFromPlayer;

        [SerializeField]
        private UnityEvent<bool> rangeStatus;
        [SerializeField]
        private UnityEvent<float> reloadStatus;
        [SerializeField]
        private UnityEvent<int> onShoot;

        public BaseUnit OwnUnit
        {
            get => _ownUnit;
            set
            {
                _ownUnit = value;

                Setup();
            }
        }

        public float AttackRadius
        {
            get
            {
                var gunConfig = Config.Get(OwnUnit.Name, ConfigKey.GunConfig, "base_gun");
                return Config.Get(gunConfig, ConfigKey.AttackRadius, 4f);
            }
        }

        public bool HasInRange
        {
            get => _hasInRange;
            set
            {
                if (_hasInRange == value) return;

                _hasInRange = value;
                rangeStatus.Invoke(_hasInRange);
            }
        }

        private void Setup()
        {
            unitCollisionTrigger.Collider.radius = AttackRadius;
            StartCoroutine(ShootInvoking());
        }

        private IEnumerator<WaitForSeconds> ShootInvoking()
        {
            var gunConfig = Config.Get(OwnUnit.Name, ConfigKey.GunConfig, "base_gun");
            var delay = Config.Get(gunConfig, ConfigKey.AttackSpeed, 1f);

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
                var targetObject = target.transform.parent.gameObject;
                var hit = Physics2D.Raycast
                (
                    position,
                    target.transform.position - position,
                    AttackRadius
                );

                if (minDist > distance && hit.transform is not null && hit.collider.gameObject == targetObject)
                {
                    minDist = distance;
                    enemy = target;
                }
            }

            if (enemy is not null)
            {
                ShootTo(enemy);
                onShoot.Invoke(AnimationKey.ShootTrigger);
            }
        }

        private void ShootTo(in BaseUnit enemy)
        {
            var gunConfig = Config.Get(OwnUnit.Name, ConfigKey.GunConfig, "base_gun");
            var scatter = Config.Get(gunConfig, ConfigKey.Scatter, 0f);
            var count = Config.Get(gunConfig, ConfigKey.BulletsCount, 0f);

            for (var i = 0; i < count; i++)
            {
                var range = Random.Range(-scatter, scatter);
                var direction = (enemy.Position - ownUnit.Position).Rotate(range).normalized;

                var bullet = Instantiate
                (
                    bulletPrefab,
                    gunBarrel.position,
                    Quaternion.Euler(0, 0, Mathf.Sign(direction.y) * Vector2.Angle(direction, Vector2.right))
                );

                bullet.Setup(OwnUnit);
                bullet.Push(direction, isFromPlayer);
            }
        }

        private float Distance(in BaseUnit target)
        {
            return Vector2.Distance(target.Position, ownUnit.Position);
        }

        public void UpdateRangeStatus()
        {
            HasInRange = !unitCollisionTrigger.CollidersInRange.IsEmpty();
        }
    }
}