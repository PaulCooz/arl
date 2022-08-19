using Abstracts.Models.CollisionTriggers;
using Common.Keys;
using UnityEngine;

namespace Realisations.Models.CollisionTriggers
{
    public class UnitCollisionTrigger : BaseCollisionTrigger
    {
        [SerializeField]
        private bool isTriggerEnemy;

        protected override bool IsTrigger(in Collider2D other)
        {
            return (isTriggerEnemy && other.CompareTag(Tags.Enemy)) ||
                   (!isTriggerEnemy && other.CompareTag(Tags.Player));
        }
    }
}