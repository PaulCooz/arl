using Common.Editor;
using Common.Keys;
using Common.Storages.Configs;
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
        private UnityEvent<BaseUnit> onAwakeUnit;
        [SerializeField]
        private UnityEvent<int, int> onHealthChange;
        [SerializeField]
        private UnityEvent onDie;

        public Vector2 Position => unitRigidbody.position;
        public string Name { get; set; }
        public float SpeedRatio => speed * Config.Get(Name, ConfigKey.Speed, 1f);

        public int Health
        {
            get => health;
            set
            {
                health = Mathf.Max(value, 0);

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

        protected virtual void Awake()
        {
            Health = maxHealth;
            onAwakeUnit.Invoke(this);
        }

        public void Translate(Vector2 delta)
        {
            _moveDelta += delta;
        }

        private void FixedUpdate()
        {
            unitRigidbody.velocity = _moveDelta * SpeedRatio;
            _moveDelta.x = _moveDelta.y = 0;
        }

        protected virtual void Die()
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