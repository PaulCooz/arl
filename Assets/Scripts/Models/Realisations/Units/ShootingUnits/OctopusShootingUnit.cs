using System.Collections.Generic;
using Keys;
using Models.Abstracts.Units.ShootingUnits;
using UnityEngine;

namespace Models.Realisations.Units.ShootingUnits
{
    public class OctopusShootingUnit : BaseShootingUnit
    {
        private Coroutine _moveToPlayer;
        private PlayerShootingUnit _player;

        private void Awake()
        {
            OnUnitAdd += OnUnitInRange;
            OnUnitRemove += OnUnitOutOfRange;
        }

        private IEnumerator<WaitForFixedUpdate> MoveToPlayer()
        {
            var fixedUpdate = new WaitForFixedUpdate();
            while (isActiveAndEnabled)
            {
                yield return fixedUpdate;

                Translate((_player.Position - Position).normalized * moveSpeed);
            }
        }

        private void OnUnitInRange(BaseShootingUnit unit)
        {
            if (!unit.CompareTag(Tags.Player)) return;

            _player = unit as PlayerShootingUnit;
            _moveToPlayer = StartCoroutine(MoveToPlayer());
        }

        private void OnUnitOutOfRange(BaseShootingUnit unit)
        {
            if (!unit.CompareTag(Tags.Player)) return;

            _player = null;
            StopCoroutine(_moveToPlayer);
        }

        private void Start()
        {
            StartCoroutine(Shootings());
        }

        private IEnumerator<WaitForSeconds> Shootings()
        {
            var wait = new WaitForSeconds(5f);
            while (isActiveAndEnabled)
            {
                yield return wait;

                AttackNearest();
            }
        }

        private void OnDestroy()
        {
            OnUnitAdd -= OnUnitInRange;
            OnUnitRemove -= OnUnitOutOfRange;
        }
    }
}