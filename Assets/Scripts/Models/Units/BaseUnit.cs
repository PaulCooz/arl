using UnityEngine;
using UnityEngine.Events;

namespace Models.Units
{
    public class BaseUnit : MonoBehaviour
    {
        private int _currentHealth;

        [SerializeField]
        private Rigidbody2D ownRigidbody2D;
        [SerializeField]
        private int maxHealth;

        [SerializeField]
        protected UnityEvent healthChange;

        public int MaxHealth => maxHealth;

        public int Health
        {
            get => _currentHealth;
            set
            {
                _currentHealth = Mathf.Max(value, 0);
                healthChange?.Invoke();
            }
        }

        public Rigidbody2D OwnRigidbody
        {
            get
            {
                if (ownRigidbody2D.Equals(null)) ownRigidbody2D = GetComponent<Rigidbody2D>();

                return ownRigidbody2D;
            }
        }

        protected virtual void Start()
        {
            Health = MaxHealth;
        }
    }
}