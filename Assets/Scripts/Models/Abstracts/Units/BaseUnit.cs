using UnityEngine;

namespace Models.Abstracts.Units
{
    public abstract class BaseUnit : MonoBehaviour
    {
        private int _health;

        [SerializeField]
        private int maxHealth;
        [SerializeField]
        private float moveSpeed;

        protected int Health
        {
            get => _health;
            set => _health = Mathf.Clamp(value, 0, maxHealth);
        }

        public void Translate(Vector2 delta)
        {
            var translation = (Time.deltaTime * moveSpeed) * delta;

            transform.Translate(new Vector3(translation.x, translation.y, 0), Space.World);
        }
    }
}