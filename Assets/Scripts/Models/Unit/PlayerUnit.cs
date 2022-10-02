using Common.Keys;

namespace Models.Unit
{
    public class PlayerUnit : BaseUnit
    {
        override public void Initialization()
        {
            Name = ConfigKey.Player;
            base.Initialization();
        }
    }
}