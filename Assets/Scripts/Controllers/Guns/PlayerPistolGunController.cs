using System.Collections.Generic;
using Models.Guns;
using Models.Units;
using SettingObjects.Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers.Guns
{
    public class PlayerPistolGunController : BaseGun, ISerializationCallbackReceiver
    {
        private WaitForSeconds _shootInterval;

        [SerializeField]
        private Rigidbody2D bulletPrefab;
        [SerializeField]
        private CircleCollider2D circleCollider2D;

        [SerializeField]
        private float bulletForce;
        [SerializeField]
        private float shootIntervalDuration;

        [SerializeField]
        private UnityEvent playerShootEvent;

        private void Start()
        {
            _shootInterval = new WaitForSeconds(shootIntervalDuration);

            StartCoroutine(Shooting());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var baseUnit = other.GetComponent<BaseUnit>();
            if (baseUnit is null || !baseUnit.CompareTag(Tag.Enemy)) return;

            enemies.Add(baseUnit);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var baseUnit = other.GetComponent<BaseUnit>();
            if (baseUnit is null || !baseUnit.CompareTag(Tag.Enemy)) return;

            enemies.Remove(baseUnit);
        }

        private IEnumerator<WaitForSeconds> Shooting()
        {
            while (isActiveAndEnabled)
            {
                yield return _shootInterval;

                ShootToNearestEnemy();
            }
        }

        protected override void ShootTo(BaseUnit unit)
        {
            playerShootEvent?.Invoke();
            PushBullet(unit);
        }

        private void PushBullet(in BaseUnit unit)
        {
            var force = (unit.OwnRigidbody.position - ownUnit.OwnRigidbody.position).normalized * bulletForce;
            var bullet = Instantiate
            (
                bulletPrefab,
                gunBarrel.position,
                Quaternion.Euler(0, 0, Mathf.Sign(force.y) * Vector2.Angle(force, Vector2.right))
            );

            bullet.AddForce(force);
        }

#if UNITY_EDITOR
        public void OnAfterDeserialize()
        {
        }

        public void OnBeforeSerialize()
        {
            circleCollider2D.radius = attackRadius;
        }
#endif
    }
}