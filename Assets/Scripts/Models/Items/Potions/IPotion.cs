using Models.Units;

namespace Models.Items.Potions
{
    public interface IPotion
    {
        void SetPlayer(in BaseUnit player);
        void Use();
    }
}