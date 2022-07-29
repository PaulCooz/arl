using System.Collections.Generic;
using Models.Abstracts.Units.ShootingUnits;
using UnityEngine;

namespace Models.Realisations.Units.ShootingUnits
{
    public class PlayerShootingUnit : BaseShootingUnit
    {
        public void Start()
        {
            StartCoroutine(Shootings());
        }

        private IEnumerator<WaitForSeconds> Shootings()
        {
            var wait = new WaitForSeconds(2f);
            while (isActiveAndEnabled)
            {
                yield return wait;

                AttackNearest();
            }
        }
    }
}