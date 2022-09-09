using Common.Editor;
using UnityEngine;
using UnityEngine.Events;

namespace Abstracts.Models.Unit
{
    public abstract class BaseUnit : MonoBehaviour, ISerializationCallbackReceiver
    {
        private Vector2 _moveDelta;

        [SerializeField, ReadOnly]
        private int health;

        [SerializeField]
        private Rigidbody2D unitRigidbody;
        [SerializeField]
        private float speed;
        [SerializeField]
        private int maxHealth;

        [SerializeField]
        private UnityEvent<int, int> onHealthChange;
        [SerializeField]
        private UnityEvent onDie;

        public Vector2 Position => unitRigidbody.position;

        public int Health
        {
            get => health;
            private set
            {
                health = Mathf.Clamp(value, 0, maxHealth);

                if (health <= 0)
                {
                    Die();
                }
                else
                {
                    onHealthChange.Invoke(health, maxHealth);
                }
            }
        }

        private void Awake()
        {
            Health = maxHealth;
        }

        public void Translate(Vector2 delta)
        {
            _moveDelta += delta;
        }

        public void TakeDamage(in int count)
        {
            Health -= count;
        }

        private void FixedUpdate()
        {
            unitRigidbody.velocity = _moveDelta * speed;
            _moveDelta.x = _moveDelta.y = 0;
        }

        private void Die()
        {
            onDie.Invoke();
            Destroy(transform.parent.gameObject);
        }

        public void OnBeforeSerialize()
        {
            onHealthChange.Invoke(0, maxHealth);
        }

        public void OnAfterDeserialize() { }
    }
}