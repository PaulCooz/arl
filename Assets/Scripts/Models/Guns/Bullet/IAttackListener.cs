namespace Models.Guns.Bullet
{
    public interface IAttackListener
    {
        void Handle(in AttackData attackData);
    }
}
