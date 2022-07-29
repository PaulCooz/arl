using System;
using System.Collections.Generic;
using Models.Abstracts.Guns;
using UnityEngine;
using UnityEngine.Events;

namespace Models.Abstracts.Units
{
    public abstract class BaseUnit : MonoBehaviour
    {
        private Coroutine _dieCoroutine;
        private Vector2 _moveDelta;
        private int _health;

        [SerializeField]
        private Rigidbody2D unitRigidbody;
        [SerializeField]
        private int maxHealth;
        [SerializeField]
        protected float moveSpeed;

        public event UnityAction<UnityAction> OnDie;

        private int Health
        {
            get => _health;
            set => _health = Mathf.Clamp(value, 0, maxHealth);
        }

        public Vector2 Position => unitRigidbody.position;

        protected virtual void Awake()
        {
            Health = maxHealth;
        }

        public void Translate(Vector2 delta)
        {
            _moveDelta += (Time.deltaTime * moveSpeed) * delta;
        }

        private void FixedUpdate()
        {
            if (_moveDelta == Vector2.zero) return;

            unitRigidbody.velocity += _moveDelta;
            _moveDelta = Vector2.zero;
        }

        public void TakeDamage(DamageData damageData)
        {
            Health -= damageData.damage;

            if (Health > 0 || _dieCoroutine != null) return;
            _dieCoroutine = StartCoroutine(Die());
        }

        private IEnumerator<WaitUntil> Die()
        {
            if (OnDie != null)
            {
                var invocationList = OnDie.GetInvocationList();
                var needReceive = invocationList.Length;
                var currentReceive = 0;
                foreach (var method in invocationList)
                {
                    method.DynamicInvoke(new Action(() => currentReceive++));
                }

                yield return new WaitUntil(() => currentReceive < needReceive);
            }

            Destroy(gameObject);
        }
    }
}