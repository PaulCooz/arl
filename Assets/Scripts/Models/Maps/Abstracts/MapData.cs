using System;
using Common.Editor;

namespace Models.Maps.Abstracts
{
    [Serializable]
    public struct MapData
    {
        public int height;
        public int width;
        public RoomData room;

        [ReadOnly]
        public int seed;
    }
}