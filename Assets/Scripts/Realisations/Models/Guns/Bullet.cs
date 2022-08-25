using Abstracts.Models.Unit;
using Common.Keys;
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

        private void OnTriggerEnter2D(Collider2D collider2d)
        {
            if (!collider2d.CompareTag(Tags.Wall)) return;

            Destroy(gameObject);
        }

        public void Push(in Vector2 direction, in bool isFromPlayer)
        {
            collisionTrigger.isTriggerEnemy = isFromPlayer;
            bulletRigidbody.velocity = direction * force;
        }

        public void Damage(BaseUnit unit)
        {
            unit.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}