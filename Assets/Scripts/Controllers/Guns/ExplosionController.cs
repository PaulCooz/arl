using Models.Guns.Bullet;
using SettingObjects.Keys;
using UnityEngine;

namespace Controllers.Guns
{
    public class ExplosionController : MonoBehaviour
    {
        [SerializeField]
        private AttackData attackData;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(Tag.Enemy)) return;
            if (other.TryGetComponent<IAttackListener>(out var handler))
            {
                handler.Handle(attackData);
            }
        }
    }
}