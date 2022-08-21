using Abstracts.Models.Unit;
using Realisations.Models.CollisionTriggers;
using UnityEngine;

namespace Realisations.Models.Guns
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private UnitCollisionTrigger collisionTrigger;
        [SerializeField]
        private Rigidbody2D bulletRigidbody;
        [SerializeField]
        private float force;

        public void Push(in Vector2 direction, in bool isFromPlayer)
        {
            collisionTrigger.isTriggerEnemy = isFromPlayer;
            bulletRigidbody.velocity = direction * force;
        }

        public void Damage(BaseUnit unit)
        {
            unit.TakeDamage(1);
        }
    }
}