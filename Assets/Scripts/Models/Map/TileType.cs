using System;

namespace Models.Map
{
    [Flags]
    public enum TileType : ushort
    {
        Empty = 1 << 0,
        Road = 1 << 1,
        Wall = 1 << 2,
        Exit = 1 << 3,
        Enemy = 1 << 4,
        Shop = 1 << 5,
        GoodItem = 1 << 6
    }

    public static class TileTypeExternals
    {
        public static bool Contains(this ushort flags, in TileType type)
        {
            return (flags & (ushort) type) != 0;
        }

        public static void Add(this ref ushort flags, in TileType type)
        {
            flags |= (ushort) type;
        }

        public static void Remove(this ref ushort flags, in TileType type)
        {
            flags ^= (ushort) type;
        }
    }
}
