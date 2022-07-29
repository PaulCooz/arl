namespace Abstracts.Models.Guns
{
    public interface IAttackListener
    {
        void TakeDamage(in DamageData damageData);
    }
}