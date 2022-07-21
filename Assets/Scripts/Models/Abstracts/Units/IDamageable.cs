using Models.Abstracts.Guns;

namespace Models.Abstracts.Units
{
    public interface IDamageable
    {
        void TakeDamage(in DamageData damageData);
    }
}