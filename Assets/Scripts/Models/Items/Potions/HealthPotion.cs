using Models.Units;

namespace Models.Items.Potions
{
    public class HealthPotion : IPotion
    {
        private const int HealthToAdd = 1;

        private BaseUnit _playerUnit;

        public void SetPlayer(in BaseUnit player)
        {
            _playerUnit = player;
        }

        public void Use()
        {
            _playerUnit.Health += HealthToAdd;
        }
    }
}