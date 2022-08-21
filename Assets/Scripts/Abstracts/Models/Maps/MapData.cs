using System;

namespace Abstracts.Models.Maps
{
    [Serializable]
    public struct MapData
    {
        public int height;
        public int width;
        public int seed;
        public int minRoomHeight;
        public int minRoomWidth;
    }
}