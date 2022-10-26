using System.Collections.Generic;
using Models.Unit;
using UnityEngine;
using UnityEngine.Events;

namespace Models.CollisionTriggers
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
            var unit = IsTrigger(other) ? other.GetComponentInChildren<BaseUnit>() : null;
            if (unit == null) return;

            CollidersInRange.Add(unit);
            onTriggerEnter.Invoke(unit);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var unit = IsTrigger(other) ? other.GetComponentInChildren<BaseUnit>() : null;
            if (unit == null) return;

            CollidersInRange.Remove(unit);
            onTriggerExit.Invoke(unit);
        }

        protected abstract bool IsTrigger(in Collider2D other);
    }
}