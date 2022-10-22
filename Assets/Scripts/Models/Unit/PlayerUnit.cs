using Common.Keys;

namespace Models.Unit
{
    public class PlayerUnit : BaseUnit
    {
        public override void Initialization()
        {
            Name = ConfigKey.Player;
            base.Initialization();
        }
    }
}