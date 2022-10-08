using Common.Editor;
using Common.Interpreters;
using Common.Keys;
using Common.Storages.Configs;
using UnityEngine;
using UnityEngine.Events;

namespace Models.Unit
{
    public abstract class BaseUnit : MonoBehaviour, ISerializationCallbackReceiver
    {
        private Vector2 _moveDelta;

        [SerializeField, ReadOnly]
        private int health = 0;

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
                var delta = value - health;

                health = Mathf.Max(value, 0);
                onHealthChange.Invoke(health, maxHealth);

                RunOnHpChange(delta);

                if (health == 0)
                {
                    Die();
                }
            }
        }

        public virtual void Initialization()
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
            RunOnDie();

            Destroy(transform.parent.gameObject);
        }

        private void RunOnHpChange(int delta)
        {
            var script = new Script();
            script.SetVariable(ConfigKey.OwnName, Name.ToScriptValue());
            script.SetVariable(ConfigKey.CurrentHp, health.ToScriptValue());
            script.SetVariable(ConfigKey.DeltaHp, delta.ToScriptValue());
            script.SetVariable(ConfigKey.OwnPosition, Position.ToScriptValue());
            script.Run(Config.Get(Name, ConfigKey.OnHpChange, ""));
        }

        private void RunOnDie()
        {
            var script = new Script();
            script.SetVariable(ConfigKey.OwnName, Name.ToScriptValue());
            script.SetVariable(ConfigKey.OwnPosition, Position.ToScriptValue());
            script.Run(Config.Get(Name, ConfigKey.OnDie, ""));
        }

        public void OnBeforeSerialize()
        {
            onHealthChange.Invoke(0, maxHealth);
        }

        public void OnAfterDeserialize() { }
    }
}