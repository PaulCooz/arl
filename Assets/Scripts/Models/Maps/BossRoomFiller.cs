using System;
using Models.Maps.Abstracts;

namespace Models.Maps
{
    public class BossRoomFiller : RoomFiller
    {
        public override void FillCommon(in Room room, Random random)
        {
            var exitPosX = random.Next(room.LeftTop.x + 1, room.RightBottom.x - 1);
            var exitPosY = random.Next(room.LeftTop.y + 1, room.RightBottom.y - 1);
            Array[exitPosX, exitPosY].Add(Entities.Exit);

            var playerPosX = random.Next(room.LeftTop.x + 1, room.RightBottom.x - 1);
            var playerPosY = random.Next(room.LeftTop.y + 1, room.RightBottom.y - 1);
            while (playerPosX == exitPosX && playerPosY == exitPosY)
            {
                playerPosX = random.Next(room.LeftTop.x + 1, room.RightBottom.x - 1);
                playerPosY = random.Next(room.LeftTop.y + 1, room.RightBottom.y - 1);
            }

            Array[playerPosX, playerPosY].Add(Entities.Player);

            var bossPosX = random.Next(room.LeftTop.x + 1, room.RightBottom.x - 1);
            var bossPosY = random.Next(room.LeftTop.y + 1, room.RightBottom.y - 1);
            while ((bossPosX == exitPosX && bossPosY == exitPosY) || (bossPosX == playerPosX && bossPosY == playerPosY))
            {
                bossPosX = random.Next(room.LeftTop.x + 1, room.RightBottom.x - 1);
                bossPosY = random.Next(room.LeftTop.y + 1, room.RightBottom.y - 1);
            }

            Array[bossPosX, bossPosY].Add(Entities.Enemy);

            Fill(room, null);
        }
    }
}