using System.Collections.Generic;
using Abstracts.Models.Units.ShootingUnits;
using Common.Keys;
using UnityEngine;

namespace Realisations.Models.Units.ShootingUnits
{
    public class OctopusShootingUnit : BaseShootingUnit
    {
        private Coroutine _moveToPlayer;
        private PlayerShootingUnit _player;

        protected override void Awake()
        {
            base.Awake();

            OnUnitAdd += OnUnitInRange;
            OnUnitRemove += OnUnitOutOfRange;
        }

        private IEnumerator<WaitForFixedUpdate> MoveToPlayer()
        {
            var fixedUpdate = new WaitForFixedUpdate();
            while (isActiveAndEnabled)
            {
                yield return fixedUpdate;
                if (_player == null) yield break;

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