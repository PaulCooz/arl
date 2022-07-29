using Models.Abstracts.Guns;
using Models.Abstracts.Units.ShootingUnits;
using UnityEngine;

namespace Models.Realisations.Guns
{
    public class DummyGun : BaseGun
    {
        public void Start()
        {
            OnAttack += Attack;
        }

        private void Attack(in BaseShootingUnit unityToAttack)
        {
            Debug.Log($"attack {unityToAttack}");
        }
    }
}