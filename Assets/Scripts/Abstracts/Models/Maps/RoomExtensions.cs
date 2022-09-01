namespace Abstracts.Models.Maps
{
    public static class RoomExtensions
    {
        public static bool IsBorder(this Room room, in int i, in int j, in int borderDepp = 0)
        {
            return i == room.LeftTop.x + borderDepp ||
                   i == room.RightBottom.x - 1 - borderDepp ||
                   j == room.LeftTop.y + borderDepp ||
                   j == room.RightBottom.y - 1 - borderDepp;
        }
    }
}