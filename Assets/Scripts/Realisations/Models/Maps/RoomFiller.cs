using System.Collections.Generic;
using Abstracts.Models.Maps;

namespace Realisations.Models.Maps
{
    public class RoomFiller : IRoomFiller
    {
        private readonly List<Entities>[,] array;

        public RoomFiller(in List<Entities>[,] array)
        {
            this.array = array;
        }

        public void Fill(in Room room, in bool isFirst)
        {
            for (var i = room.LeftTop.x; i < room.RightBottom.x; i++)
            for (var j = room.LeftTop.y; j < room.RightBottom.y; j++)
            {
                if (IsBorder(room, i, j))
                {
                    array[i, j].Add(Entities.Wall);
                }
                else
                {
                    array[i, j].Add(Entities.Floor);
                }
            }
        }

        private bool IsBorder(in Room room, in int i, in int j)
        {
            return i == room.LeftTop.x || i == room.RightBottom.x - 1 || j == room.LeftTop.x || j == room.RightBottom.y - 1;
        }
    }
}