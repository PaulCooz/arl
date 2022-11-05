using System;

namespace Models.Maps.Abstracts
{
    [Serializable]
    public struct RoomData
    {
        public int minHeight;
        public int minWidth;
        public int maxHeight;
        public int maxWidth;
    }
}