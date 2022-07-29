using Abstracts.Models.Guns;

namespace Abstracts.Models.Units
{
    public interface IDamageable
    {
        void TakeDamage(in DamageData damageData);
    }
}