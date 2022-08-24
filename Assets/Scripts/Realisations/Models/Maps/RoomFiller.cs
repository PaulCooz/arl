using System.Collections.Generic;
using Abstracts.Models.Maps;
using UnityEngine;

namespace Realisations.Models.Maps
{
    public class RoomFiller : IRoomFiller
    {
        private readonly List<Entities>[,] _array;

        public RoomFiller(in List<Entities>[,] array)
        {
            _array = array;
        }

        public void Fill(in Room room, in bool isFirst)
        {
            var setPlayer = false;
            for (var i = room.LeftTop.x; i < room.RightBottom.x; i++)
            for (var j = room.LeftTop.y; j < room.RightBottom.y; j++)
            {
                if (IsBorder(room, i, j))
                {
                    _array[i, j].Add(Entities.Wall);
                }
                else
                {
                    _array[i, j].Add(Entities.Floor);

                    if (isFirst && !setPlayer)
                    {
                        setPlayer = true;
                        _array[i, j].Add(Entities.Player);
                    }

                    if (!isFirst && Random.Range(0, 100) < 10)
                    {
                        _array[i, j].Add(Entities.Enemy);
                    }
                }
            }
        }

        private bool IsBorder(in Room room, in int i, in int j)
        {
            return i == room.LeftTop.x || i == room.RightBottom.x - 1 || j == room.LeftTop.y || j == room.RightBottom.y - 1;
        }
    }
}