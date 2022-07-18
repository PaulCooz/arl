using UnityEngine;

namespace Models.Abstracts.Guns.Bullets
{
    public abstract class BaseBullet : MonoBehaviour
    {
        [SerializeField]
        public DamageData damageData;
    }
}