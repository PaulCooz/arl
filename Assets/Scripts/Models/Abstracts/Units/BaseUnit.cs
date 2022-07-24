using UnityEngine;

namespace Models.Abstracts.Units
{
    public abstract class BaseUnit : MonoBehaviour
    {
        private Vector2 _moveDelta;
        private int _health;

        [SerializeField]
        private Rigidbody2D unitRigidbody;
        [SerializeField]
        private int maxHealth;
        [SerializeField]
        protected float moveSpeed;

        protected int Health
        {
            get => _health;
            set => _health = Mathf.Clamp(value, 0, maxHealth);
        }

        public Vector2 Position => unitRigidbody.position;

        public void Translate(Vector2 delta)
        {
            _moveDelta = (Time.deltaTime * moveSpeed) * delta;
        }

        private void FixedUpdate()
        {
            if (_moveDelta == Vector2.zero) return;

            unitRigidbody.velocity += _moveDelta;
            _moveDelta = Vector2.zero;
        }
    }
}