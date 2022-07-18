using Models.Abstracts.Guns.Bullets;
using Models.Abstracts.Units.ShootingUnits;
using UnityEngine;

namespace Models.Abstracts.Guns
{
    public abstract class BaseGun : MonoBehaviour
    {
        [SerializeField]
        protected BaseBullet bullet;

        public void AttackTo(in BaseShootingUnit nearest)
        {
            nearest.TakeDamage(bullet.damageData);
        }
    }
}