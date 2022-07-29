using Abstracts.Models.Guns.Bullets;
using Abstracts.Models.Units.ShootingUnits;
using UnityEngine;

namespace Abstracts.Models.Guns
{
    public abstract class BaseGun : MonoBehaviour
    {
        public delegate void AttackAction(in BaseShootingUnit unit);

        [SerializeField]
        private Collider2D ownCollider;

        [SerializeField]
        private Transform gunBarrel;
        [SerializeField]
        private bool isHasBullet;

        [SerializeField]
        protected BaseBullet bulletPrefab;

        private Vector2 BarrelPosition => gunBarrel.position;

        public event AttackAction OnAttack;

        public void AttackTo(in BaseShootingUnit nearest)
        {
            OnAttack?.Invoke(nearest);

            if (isHasBullet) PushBulletTo(nearest.Position);
        }

        private void PushBulletTo(in Vector2 position)
        {
            var bullet = Instantiate(bulletPrefab, transform);
            var delta = position - BarrelPosition;

            bullet.transform.position = BarrelPosition;
            bullet.transform.rotation = Quaternion.Euler(0, 0, Angle(-delta));

            bullet.IgnoreCollision(ownCollider);
            bullet.Push(delta.normalized);
        }

        private float Angle(in Vector2 delta)
        {
            return Mathf.Atan(delta.y / delta.x) * Mathf.Rad2Deg;
        }
    }
}