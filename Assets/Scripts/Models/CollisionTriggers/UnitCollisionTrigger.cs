using Common.Keys;
using UnityEngine;

namespace Models.CollisionTriggers
{
    public class UnitCollisionTrigger : BaseCollisionTrigger
    {
        [SerializeField]
        public bool isTriggerEnemy;

        protected override bool IsTrigger(in Collider2D other)
        {
            return (isTriggerEnemy && other.CompareTag(Tags.Enemy)) ||
                   (!isTriggerEnemy && other.CompareTag(Tags.Player));
        }
    }
}