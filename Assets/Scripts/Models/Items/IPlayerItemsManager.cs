namespace Models.Items
{
    public interface IPlayerItemsManager
    {
        void Use(in int id, in ItemObject itemObject);
    }
}