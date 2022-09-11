using System.Collections.Generic;

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

        public static bool IsConnected(this Room roomA, in Room roomB)
        {
            var check = new List<Room> {roomA};
            foreach (var neighbour in roomA.Neighbours)
            {
                if (CheckConnection(neighbour, roomB, ref check)) return true;
            }

            return false;
        }

        private static bool CheckConnection(in Room current, in Room find, ref List<Room> check)
        {
            if (check.Contains(current)) return false;
            check.Add(current);

            if (current == find) return true;

            foreach (var neighbour in current.Neighbours)
            {
                if (CheckConnection(neighbour, find, ref check)) return true;
            }

            return false;
        }
    }
}