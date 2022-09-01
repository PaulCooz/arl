﻿using System.Collections.Generic;
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

        public void Fill(in Room room, in bool isFirst, in bool isLast)
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

                    if (isFirst && !setPlayer)
                    {
                        setPlayer = true;
                        _array[i, j].Add(Entities.Player);
                    }

                    if (isLast && !setExit && !room.IsBorder(i, j, 1))
                    {
                        setExit = true;
                        _array[i, j].Add(Entities.Exit);
                    }

                    if (!isFirst && Random.Range(0, 100) < 10)
                    {
                        _array[i, j].Add(Entities.Enemy);
                    }
                }
            }
        }

    }
}