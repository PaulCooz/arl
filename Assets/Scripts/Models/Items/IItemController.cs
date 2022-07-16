namespace Models.Items
{
    public interface IItemController
    {
        void Init(in ItemObject data);
        void OnItemClick();
    }
}