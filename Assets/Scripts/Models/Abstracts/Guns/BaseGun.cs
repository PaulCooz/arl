using Models.Abstracts.Guns.Bullets;
using Models.Abstracts.Units.ShootingUnits;
using UnityEngine;

namespace Models.Abstracts.Guns
{
    public abstract class BaseGun : MonoBehaviour
    {
        public delegate void AttackAction(in BaseShootingUnit unit);

        [SerializeField]
        protected BaseBullet bullet;
        [SerializeField]
        private bool isHasBullet;

        public event AttackAction OnAttack;

        public void AttackTo(in BaseShootingUnit nearest)
        {
            OnAttack?.Invoke(nearest);

            if (isHasBullet) nearest.TakeDamage(bullet.damageData);
        }
    }
}