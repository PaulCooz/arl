using Models.Guns.Bullet;
using UnityEngine;
using Views.Effects;

namespace Controllers.Guns.Bullet
{
    public class RocketBulletController : BulletController
    {
        [SerializeField]
        private ExplosionEffectView explosionEffect;

        protected override void Attack(IAttackListener handler)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

            base.Attack(handler);
        }

        protected override void CollideWithWall()
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

            base.CollideWithWall();
        }
    }
}