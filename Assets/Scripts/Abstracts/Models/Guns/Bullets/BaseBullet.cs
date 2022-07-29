using UnityEngine;

namespace Models.Abstracts.Guns.Bullets
{
    public abstract class BaseBullet : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D bulletRigidbody;
        [SerializeField]
        private Collider2D bulletCollider;
        [SerializeField]
        public DamageData damageData;
        [SerializeField]
        private float force;

        public void IgnoreCollision(in Collider2D collider2d)
        {
            Physics2D.IgnoreCollision(bulletCollider, collider2d);
        }

        public void Push(in Vector2 direction)
        {
            bulletRigidbody.AddForce(force * direction);
        }

        private void OnCollisionEnter2D(Collision2D collider2d)
        {
            var unit = collider2d.gameObject.GetComponentInChildren<IAttackListener>();
            unit?.TakeDamage(damageData);
        }
    }
}