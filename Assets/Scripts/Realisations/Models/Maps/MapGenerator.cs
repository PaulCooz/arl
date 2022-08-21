using System;
using System.Collections.Generic;
using Abstracts.Models.Maps;
using Common.Arrays;

namespace Realisations.Models.Maps
{
    public class MapGenerator : IMapGenerator
    {
        private BinaryTree<Room> roomTree;
        private Random random;
        private MapData data;

        public Map GetNextMap(in List<Entities>[,] array, in MapData mapData, in IRoomFiller roomFiller)
        {
            data = mapData;
            random = new Random(data.seed);

            var mainRoom = new Room(data.height, data.width, 0, 0, data.height, data.width);
            roomTree = new BinaryTree<Room>(mainRoom);

            CreateNextRooms(0, mainRoom.Height, 0, mainRoom.Width);

            var rooms = roomTree.GetAllLeaves();
            for (var i = 0; i < rooms.Count; i++)
            {
                roomFiller.Fill(rooms[i], i == 0);
            }

            return new Map(array, mainRoom, data.height, data.width);
        }

        private void CreateNextRooms(int minX, int maxX, int minY, int maxY)
        {
            var room = roomTree.CurrentValue;
            if (!DivideRoom(minX, maxX, minY, maxY, room, out var leftRoom, out var rightRoom)) return;

            roomTree.AddNode(leftRoom, true);
            roomTree.Down(true);
            CreateNextRooms(leftRoom.LeftTop.x, leftRoom.RightBottom.x, leftRoom.LeftTop.y, leftRoom.RightBottom.y);
            roomTree.Up();

            roomTree.AddNode(rightRoom, false);
            roomTree.Down(false);
            CreateNextRooms(rightRoom.LeftTop.x, rightRoom.RightBottom.x, rightRoom.LeftTop.y, rightRoom.RightBottom.y);
            roomTree.Up();
        }

        private bool DivideRoom(int minX, int maxX, int minY, int maxY, Room room, out Room leftRoom, out Room rightRoom)
        {
            var isDivByHeight = room.Height > room.Width;
            if (isDivByHeight)
            {
                var midH = room.Height / 2;
                if (midH < data.minRoomHeight)
                {
                    leftRoom = null;
                    rightRoom = null;
                    return false;
                }

                leftRoom = new Room(midH, room.Width, minX, minX + midH, minY, maxY);
                rightRoom = new Room(midH, room.Width, minX + midH, maxX, minY, maxY);
            }
            else
            {
                var midW = room.Width / 2;
                if (midW < data.minRoomWidth)
                {
                    leftRoom = null;
                    rightRoom = null;
                    return false;
                }

                leftRoom = new Room(room.Height, midW, minX, maxX, minY, minY + midW);
                rightRoom = new Room(room.Height, midW, minX, maxX, minY + midW, maxY);
            }

            return true;
        }
    }
}