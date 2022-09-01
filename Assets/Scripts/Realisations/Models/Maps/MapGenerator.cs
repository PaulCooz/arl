using System.Collections.Generic;
using Abstracts.Models.Maps;
using Common.Arrays;
using UnityEngine;
using Random = System.Random;

namespace Realisations.Models.Maps
{
    public class MapGenerator : IMapGenerator
    {
        private BinaryTree<Room> _roomTree;
        private Random _random;
        private MapData _data;

        public Map GetNextMap(in List<Entities>[,] array, in MapData mapData, in IRoomFiller roomFiller)
        {
            _data = mapData;
            _random = new Random(_data.seed);

            var mainRoom = new Room(_data.height, _data.width, 0, 0, _data.height, _data.width);
            _roomTree = new BinaryTree<Room>(mainRoom);

            CreateNextRooms(0, mainRoom.Height, 0, mainRoom.Width);

            var rooms = _roomTree.GetAllLeaves();
            for (var i = 0; i < rooms.Count; i++)
            {
                roomFiller.Fill(rooms[i], i == 0, i == rooms.Count - 1);
            }

            var map = new Map(array, mainRoom, _data.height, _data.width);
            ConnectRooms(ref map, rooms);

            return map;
        }

        private void CreateNextRooms(int minX, int maxX, int minY, int maxY)
        {
            var room = _roomTree.CurrentValue;
            if (!DivideRoom(minX, maxX, minY, maxY, room, out var leftRoom, out var rightRoom)) return;

            _roomTree.AddNode(leftRoom, true);
            _roomTree.Down(true);
            CreateNextRooms(leftRoom.LeftTop.x, leftRoom.RightBottom.x, leftRoom.LeftTop.y, leftRoom.RightBottom.y);
            _roomTree.Up();

            _roomTree.AddNode(rightRoom, false);
            _roomTree.Down(false);
            CreateNextRooms(rightRoom.LeftTop.x, rightRoom.RightBottom.x, rightRoom.LeftTop.y, rightRoom.RightBottom.y);
            _roomTree.Up();
        }

        private bool DivideRoom(int minX, int maxX, int minY, int maxY, Room room, out Room leftRoom, out Room rightRoom)
        {
            var isDivByHeight = room.Height > room.Width;
            if (isDivByHeight)
            {
                var midH = room.Height / 2;
                if (midH < _data.minRoomHeight)
                {
                    leftRoom = null;
                    rightRoom = null;
                    return false;
                }

                leftRoom = new Room(midH, room.Width, minX, minX + midH, minY, maxY);
                rightRoom = new Room(midH + (room.Height & 1), room.Width, minX + midH, maxX, minY, maxY);
            }
            else
            {
                var midW = room.Width / 2;
                if (midW < _data.minRoomWidth)
                {
                    leftRoom = null;
                    rightRoom = null;
                    return false;
                }

                leftRoom = new Room(room.Height, midW, minX, maxX, minY, minY + midW);
                rightRoom = new Room(room.Height, midW + (room.Width & 1), minX, maxX, minY + midW, maxY);
            }

            return true;
        }

        private void ConnectRooms(ref Map map, in List<Room> rooms)
        {
            var connected = new bool[rooms.Count, rooms.Count];
            for (var i = 0; i < rooms.Count; i++)
            for (var j = 0; j < rooms.Count; j++)
            {
                connected[i, j] = i == j;
            }

            for (var i = 0; i < rooms.Count; i++)
            for (var j = 0; j < rooms.Count; j++)
            {
                if (connected[i, j]) continue;

                var leftTopI = rooms[i].LeftTop;
                var leftTopJ = rooms[j].LeftTop;
                var rightBottomI = rooms[i].RightBottom;
                var rightBottomJ = rooms[j].RightBottom;

                var minCommonX = Mathf.Max(leftTopI.x, leftTopJ.x) + 1;
                var maxCommonX = Mathf.Min(rightBottomI.x, rightBottomJ.x) - 1;
                var minCommonY = Mathf.Max(leftTopI.y, leftTopJ.y) + 1;
                var maxCommonY = Mathf.Min(rightBottomI.y, rightBottomJ.y) - 1;

                if (minCommonX < maxCommonX)
                {
                    for (var h = minCommonX; h < maxCommonX; h++)
                    {
                        if (!CanDoRow(h, map, rooms[i], rooms[j])) continue;

                        DoRow(h, map, rooms[i], rooms[j]);
                        connected[i, j] = connected[j, i] = true;
                        break;
                    }
                }
                else if (minCommonY < maxCommonY)
                {
                    for (var w = minCommonY; w < maxCommonY; w++)
                    {
                        if (!CanDoColumn(w, map, rooms[i], rooms[j])) continue;

                        DoColumn(w, map, rooms[i], rooms[j]);
                        connected[i, j] = connected[j, i] = true;
                        break;
                    }
                }
            }
        }

        private bool CanDoRow(in int h, in Map map, Room roomA, Room roomB)
        {
            if (roomA.RightBottom.y >= roomB.LeftTop.y) (roomA, roomB) = (roomB, roomA);
            for (var w = roomA.RightBottom.y + 1; w < roomB.LeftTop.y - 1; w++)
            {
                if (map[h, w].Contains(Entities.Wall)) return false;
            }

            return true;
        }

        private void DoRow(in int h, in Map map, Room roomA, Room roomB)
        {
            if (roomA.RightBottom.y > roomB.LeftTop.y) (roomA, roomB) = (roomB, roomA);
            for (var w = roomA.RightBottom.y - 1; w <= roomB.LeftTop.y; w++)
            {
                if (map[h, w].Contains(Entities.Wall)) map[h, w].Remove(Entities.Wall);
                map[h, w].Add(Entities.Floor);
            }
        }

        private bool CanDoColumn(in int w, in Map map, Room roomA, Room roomB)
        {
            if (roomA.RightBottom.x > roomB.LeftTop.x) (roomA, roomB) = (roomB, roomA);
            for (var h = roomA.RightBottom.x + 1; h < roomB.LeftTop.x - 1; h++)
            {
                if (map[h, w].Contains(Entities.Wall)) return false;
            }

            return true;
        }

        private void DoColumn(in int w, in Map map, Room roomA, Room roomB)
        {
            if (roomA.RightBottom.x > roomB.LeftTop.x) (roomA, roomB) = (roomB, roomA);
            for (var h = roomA.RightBottom.x - 1; h <= roomB.LeftTop.x; h++)
            {
                if (map[h, w].Contains(Entities.Wall)) map[h, w].Remove(Entities.Wall);
                map[h, w].Add(Entities.Floor);
            }
        }
    }
}