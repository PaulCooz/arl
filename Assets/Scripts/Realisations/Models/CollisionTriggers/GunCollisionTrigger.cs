using Abstracts.Models.CollisionTriggers;
using Common.Keys;
using UnityEngine;

namespace Realisations.Models.CollisionTriggers
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class GunCollisionTrigger : BaseCollisionTrigger
    {
        [SerializeField]
        private CircleCollider2D circleCollider2d;
        [SerializeField]
        private bool isTriggerEnemy;

        public CircleCollider2D Collider => circleCollider2d;

        protected override bool IsTrigger(in Collider2D other)
        {
            return (isTriggerEnemy && other.CompareTag(Tags.Enemy)) ||
                   (!isTriggerEnemy && other.CompareTag(Tags.Player));
        }
    }
}