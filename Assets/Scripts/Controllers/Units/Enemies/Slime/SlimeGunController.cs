using System;
using System.Collections;
using Models.Guns;
using Models.Guns.Bullet;
using Models.Units;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers.Units.Enemies.Slime
{
    public class SlimeGunController : BaseGun
    {
        private WaitForSeconds _shootInterval;
        private Action _delayedDamage;

        [SerializeField]
        private SlimeUnitController slimeUnit;

        [SerializeField]
        private float shootIntervalDuration;
        [SerializeField]
        private float startShootMaxDelay;
        [SerializeField]
        private AttackData attackData;
        [SerializeField]
        private UnityEvent shootEvent;

        public void Init(in BaseUnit player)
        {
            _shootInterval = new WaitForSeconds(shootIntervalDuration);
            enemies.Add(player);

            StartCoroutine(Shooting());
        }

        private IEnumerator Shooting()
        {
            yield return new WaitForSeconds((float) slimeUnit.CurrentRandom.NextDouble() * startShootMaxDelay);

            while (isActiveAndEnabled)
            {
                yield return _shootInterval;

                ShootToNearestEnemy();
            }
        }

        protected override void ShootTo(BaseUnit unit)
        {
            shootEvent?.Invoke();
            _delayedDamage += () => unit.GetComponent<IAttackListener>().Handle(attackData);
        }

        public void Damage() // called in attack anim
        {
            _delayedDamage?.Invoke();
            _delayedDamage = null;
        }
    }
}
