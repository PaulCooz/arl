using System.Collections.Generic;
using Common;
using Models.Maps.Abstracts;

namespace Models.Maps
{
    public class RoomFiller : IRoomFiller
    {
        protected delegate void SetEntity(in int i, in int j);

        protected List<Entities>[,] Array;

        public void Setup(in List<Entities>[,] array)
        {
            Array = array;
        }

        public void FillStart(in Room room, System.Random random)
        {
            var posX = random.Next(room.LeftTop.x + 1, room.RightBottom.x - 1);
            var posY = random.Next(room.LeftTop.y + 1, room.RightBottom.y - 1);

            Array[posX, posY].Add(Entities.Player);

            Fill(room, null);
        }

        public virtual void FillCommon(in Room room, System.Random random)
        {
            Fill
            (
                room,
                (in int i, in int j) =>
                {
                    if (random.Chance(10))
                    {
                        Array[i, j].Add(Entities.Enemy);
                    }
                }
            );
        }

        public void FillExit(in Room room, System.Random random)
        {
            var posX = random.Next(room.LeftTop.x + 1, room.RightBottom.x - 1);
            var posY = random.Next(room.LeftTop.y + 1, room.RightBottom.y - 1);

            Array[posX, posY].Add(Entities.Exit);

            Fill
            (
                room,
                (in int i, in int j) =>
                {
                    if ((posX != i || posY != j) && random.Chance(5))
                    {
                        Array[i, j].Add(Entities.Enemy);
                    }
                }
            );
        }

        protected void Fill(Room room, SetEntity setEntity)
        {
            for (var i = room.LeftTop.x; i < room.RightBottom.x; i++)
            for (var j = room.LeftTop.y; j < room.RightBottom.y; j++)
            {
                if (room.IsBorder(i, j))
                {
                    Array[i, j].Add(Entities.Wall);
                }
                else
                {
                    Array[i, j].Add(Entities.Floor);

                    setEntity?.Invoke(i, j);
                }
            }
        }
    }
}