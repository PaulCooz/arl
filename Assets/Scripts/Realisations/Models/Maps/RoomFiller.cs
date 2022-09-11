using System.Collections.Generic;
using Abstracts.Models.Maps;
using Common;

namespace Realisations.Models.Maps
{
    public class RoomFiller : IRoomFiller
    {
        private readonly List<Entities>[,] _array;

        public RoomFiller(in List<Entities>[,] array)
        {
            _array = array;
        }

        public void Fill(in Room room, in bool isStart, in bool isExit, in System.Random random)
        {
            var setPlayer = false;
            var setExit = false;
            for (var i = room.LeftTop.x; i < room.RightBottom.x; i++)
            for (var j = room.LeftTop.y; j < room.RightBottom.y; j++)
            {
                if (room.IsBorder(i, j))
                {
                    _array[i, j].Add(Entities.Wall);
                }
                else
                {
                    _array[i, j].Add(Entities.Floor);

                    if (isStart && !setPlayer)
                    {
                        setPlayer = true;
                        _array[i, j].Add(Entities.Player);
                    }

                    if (isExit && !setExit && !room.IsBorder(i, j, 1))
                    {
                        setExit = true;
                        _array[i, j].Add(Entities.Exit);
                    }

                    if (!isStart && random.Chance(10))
                    {
                        _array[i, j].Add(Entities.Enemy);
                    }
                }
            }
        }
    }
}