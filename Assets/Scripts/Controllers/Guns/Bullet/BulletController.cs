using Models.Guns.Bullet;
using SettingObjects.Keys;
using UnityEngine;

namespace Controllers.Guns.Bullet
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField]
        protected AttackData attackData;

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(Tag.Enemy) && !other.CompareTag(Tag.Wall)) return;
            if (other.TryGetComponent<IAttackListener>(out var handler))
            {
                Attack(handler);
            }
            else
            {
                CollideWithWall();
            }

            Destroy(gameObject);
        }

        protected virtual void Attack(IAttackListener handler)
        {
            handler.Handle(attackData);
        }

        protected virtual void CollideWithWall()
        {
        }
    }
}