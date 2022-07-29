using Abstracts.Models.Guns;
using Abstracts.Models.Units.ShootingUnits;
using UnityEngine;

namespace Realisations.Models.Guns
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