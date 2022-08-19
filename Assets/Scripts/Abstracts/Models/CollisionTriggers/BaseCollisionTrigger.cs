using System.Collections.Generic;
using Abstracts.Models.Unit;
using UnityEngine;
using UnityEngine.Events;

namespace Abstracts.Models.CollisionTriggers
{
    public abstract class BaseCollisionTrigger : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent<BaseUnit> onTriggerEnter;
        [SerializeField]
        private UnityEvent<BaseUnit> onTriggerExit;

        public List<BaseUnit> CollidersInRange { get; } = new();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!IsTrigger(other) || !other.TryGetComponent<BaseUnit>(out var unit)) return;

            CollidersInRange.Add(unit);
            onTriggerEnter.Invoke(unit);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!IsTrigger(other) || !other.TryGetComponent<BaseUnit>(out var unit)) return;

            CollidersInRange.Remove(unit);
            onTriggerExit.Invoke(unit);
        }

        protected abstract bool IsTrigger(in Collider2D other);
    }
}