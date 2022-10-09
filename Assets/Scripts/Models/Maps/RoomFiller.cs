using System.Collections.Generic;
using Common;
using Models.Maps.Abstracts;
using UnityEngine;

namespace Models.Maps
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
            var unique = (Vector2Int?) null;
            if (isStart) SetPlayer(room, random, out unique);
            if (isExit) SetExit(room, random, out unique);

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

                    if (unique.HasValue && unique.Value.x == i && unique.Value.y == j) continue;

                    if (!isStart && random.Chance(10))
                    {
                        _array[i, j].Add(Entities.Enemy);
                    }
                }
            }
        }

        private void SetPlayer(in Room room, in System.Random random, out Vector2Int? unique)
        {
            var posX = random.Next(room.LeftTop.x + 1, room.RightBottom.x - 1);
            var posY = random.Next(room.LeftTop.y + 1, room.RightBottom.y - 1);

            _array[posX, posY].Add(Entities.Player);
            unique = new Vector2Int(posX, posY);
        }

        private void SetExit(in Room room, in System.Random random, out Vector2Int? unique)
        {
            var posX = random.Next(room.LeftTop.x + 1, room.RightBottom.x - 1);
            var posY = random.Next(room.LeftTop.y + 1, room.RightBottom.y - 1);

            _array[posX, posY].Add(Entities.Exit);
            unique = new Vector2Int(posX, posY);
        }
    }
}