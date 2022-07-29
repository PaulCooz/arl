using Abstracts.Models.Guns;
using UnityEngine;
using UnityEngine.Events;

namespace Realisations.Models.Guns
{
    public class AttackListener : MonoBehaviour, IAttackListener
    {
        public UnityEvent<DamageData> onAttack;

        public void TakeDamage(in DamageData damageData)
        {
            onAttack?.Invoke(damageData);
        }
    }
}