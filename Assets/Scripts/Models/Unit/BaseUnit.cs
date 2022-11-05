using Common;
using Common.Configs;
using Common.Editor;
using Common.Interpreters;
using Common.Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Models.Unit
{
    public abstract class BaseUnit : MonoBehaviour
    {
        private Vector2 _moveDelta;
        private bool _isRun = false;
        private bool _isLockLeft = false;

        [SerializeField, ReadOnly]
        private int health = 0;

        [SerializeField]
        private Rigidbody2D unitRigidbody;
        [SerializeField]
        private float speed;

        [SerializeField]
        private UnityEvent<BaseUnit> onAwakeUnit;
        [SerializeField]
        protected UnityEvent<int> onHealthChange;

        [SerializeField]
        private UnityEvent<bool> turnLeft;
        [SerializeField]
        private UnityEvent<int, bool> onRun;
        [SerializeField]
        private UnityEvent<int> onDie;

        public Vector2 Position => unitRigidbody.position;
        public UnitConfigObject UnitConfig { get; set; }
        public float SpeedRatio => speed * UnitConfig.speed;
        public bool IsRun
        {
            get => _isRun;
            set
            {
                if (_isRun == value) return;

                _isRun = value;
                onRun.Invoke(AnimationKey.RunProp, _isRun);
            }
        }
        public bool IsLockLeft
        {
            get => _isLockLeft;
            set
            {
                if (_isLockLeft == value) return;

                _isLockLeft = value;
                turnLeft.Invoke(_isLockLeft);
            }
        }
        public int Health
        {
            get => health;
            set
            {
                var next = Mathf.Max(value, 0);
                var delta = next - health;
                if (delta == 0) return;

                health = next;
                onHealthChange.Invoke(health);

                RunOnHpChange(delta);

                if (health == 0) Die();
            }
        }

        protected virtual void PreInitAndSetHealth()
        {
            Health = UnitConfig.health;
        }

        public void Initialization()
        {
            PreInitAndSetHealth();
            enabled = true;
            onAwakeUnit.Invoke(this);
        }

        public void Translate(Vector2 delta)
        {
            _moveDelta += delta * Time.deltaTime;
        }

        private void FixedUpdate()
        {
            unitRigidbody.velocity = _moveDelta * SpeedRatio;

            CheckState(unitRigidbody.velocity);
            _moveDelta.x = _moveDelta.y = 0;
        }

        private void CheckState(Vector2 velocity)
        {
            IsRun = velocity.NotZero();
            IsLockLeft = velocity.x switch
            {
                < 0f => true,
                > 0f => false,
                _ => IsLockLeft
            };
        }

        private void Die()
        {
            onDie.Invoke(AnimationKey.DieTrigger);
            if (Health == 0) RunOnDie();
        }

        public void UnitDestroy()
        {
            Destroy(transform.parent.gameObject);
        }

        private void RunOnHpChange(int delta)
        {
            var script = new Script();
            script.SetVariable(ConfigKey.CurrentHp, health.ToScriptValue());
            script.SetVariable(ConfigKey.DeltaHp, delta.ToScriptValue());
            script.SetVariable(ConfigKey.OwnPosition, Position.ToScriptValue());
            script.Run(UnitConfig.onHpChange);
        }

        private void RunOnDie()
        {
            var script = new Script();
            script.SetVariable(ConfigKey.OwnPosition, Position.ToScriptValue());
            script.SetVariable(ConfigKey.LevelExp, UnitConfig.levelExp.ToScriptValue());
            script.Run(UnitConfig.onDie);
        }
    }
}