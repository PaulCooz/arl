using UnityEngine;

namespace Models.Map
{
    public struct Room
    {
        public readonly int Width;
        public readonly int Height;
        public readonly Vector2Int Center;

        public int MinX => Center.x - ((Width | 1) - 1) / 2;
        public int MaxX => Center.x + (Width - 1) / 2;
        public int MinY => Center.y - ((Height | 1) - 1) / 2;
        public int MaxY => Center.y + (Height - 1) / 2;

        public Room(Vector2Int center, int width, int height)
        {
            Center = center;
            Width = width;
            Height = height;
        }
    }
}
