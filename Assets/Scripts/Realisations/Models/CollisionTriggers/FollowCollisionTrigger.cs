using System.Linq;
using Abstracts.Models.CollisionTriggers;
using Abstracts.Models.Unit;
using Common.Arrays;
using UnityEngine;

namespace Realisations.Models.CollisionTriggers
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class FollowCollisionTrigger : BaseCollisionTrigger
    {
        [SerializeField]
        private BaseUnit baseUnit;
        [SerializeField]
        private string[] tags;
        [SerializeField]
        private float minDistance;
        [SerializeField]
        private float speed;

        protected override bool IsTrigger(in Collider2D other)
        {
            return tags.Any(other.CompareTag);
        }

        private void Update()
        {
            if (CollidersInRange.IsEmpty()) return;

            var attractor = CollidersInRange.Front();
            var distance = Vector2.Distance(baseUnit.Position, attractor.Position);
            if (distance <= minDistance) return;

            // ReSharper disable once Unity.PreferNonAllocApi
            var hits = Physics2D.RaycastAll(baseUnit.Position, attractor.Position - baseUnit.Position);
            var hit = (RaycastHit2D?) null;
            foreach (var h in hits)
            {
                if (h.collider.isTrigger) continue;

                hit = h;
                break;
            }

            if (hit?.transform is null || hit.Value.collider.gameObject != attractor.transform.parent.gameObject) return;

            var len = speed * Time.deltaTime;
            baseUnit.Translate(len * (attractor.Position - baseUnit.Position).normalized);
        }
    }
}