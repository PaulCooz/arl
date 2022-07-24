namespace Models.Abstracts.Guns
{
    public interface IAttackListener
    {
        void TakeDamage(in DamageData damageData);
    }
}