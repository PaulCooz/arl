using Models.Abstracts.Guns;
using UnityEngine;
using UnityEngine.Events;

namespace Models.Realisations.Guns
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