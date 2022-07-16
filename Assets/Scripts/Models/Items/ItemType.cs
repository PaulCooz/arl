namespace Models.Items
{
    public enum ItemType : byte
    {
        None = 0,
        Potion = 1 << 1,
        Passive = 1 << 2
    }
}
