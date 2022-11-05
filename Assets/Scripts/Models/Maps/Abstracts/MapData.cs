using System;

namespace Models.Maps.Abstracts
{
    [Serializable]
    public struct MapData
    {
        public int height;
        public int width;
        public RoomData room;

        public int seed;
    }
}