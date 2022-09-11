using System.Collections.Generic;
using Abstracts.Models.Maps;
using Common;
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
            var start = _random.Next(0, rooms.Count);
            var exit = _random.Next(0, rooms.Count);
            if (start == exit) exit = (exit + 1) % rooms.Count;

            FillRooms(roomFiller, rooms, start, exit);

            var map = new Map(array, mainRoom, _data.height, _data.width);
            ConnectRooms(ref map, rooms, start, exit);

            return map;
        }

        private void FillRooms(in IRoomFiller roomFiller, in IReadOnlyList<Room> rooms, in int start, in int exit)
        {
            for (var i = 0; i < rooms.Count; i++)
            {
                roomFiller.Fill(rooms[i], i == start, i == exit, _random);
            }
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

        private void ConnectRooms(ref Map map, in List<Room> rooms, in int start, in int exit)
        {
            for (var i = 0; i < rooms.Count; i++)
            for (var j = i + 1; j < rooms.Count; j++)
            {
                if ((rooms[i].IsConnected(rooms[j]) && _random.Chance(50)) ||
                    ((i == start || i == exit) && rooms[i].IsImpasse) ||
                    ((j == start || j == exit) && rooms[j].IsImpasse)) continue;

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
                    var row = (int?) null;
                    for (var h = minCommonX; h < maxCommonX; h++)
                    {
                        if (CanDoRow(h, map, rooms[i], rooms[j]) && (!row.HasValue || !_random.Chance(50)))
                        {
                            row = h;
                        }
                    }

                    if (row.HasValue)
                    {
                        DoRow(row.Value, map, rooms[i], rooms[j]);
                        rooms[i].Neighbours.Add(rooms[j]);
                        rooms[j].Neighbours.Add(rooms[i]);
                    }
                }
                else if (minCommonY < maxCommonY)
                {
                    var column = (int?) null;
                    for (var w = minCommonY; w < maxCommonY; w++)
                    {
                        if (CanDoColumn(w, map, rooms[i], rooms[j]) && (!column.HasValue || !_random.Chance(50)))
                        {
                            column = w;
                        }
                    }

                    if (column.HasValue)
                    {
                        DoColumn(column.Value, map, rooms[i], rooms[j]);
                        rooms[i].Neighbours.Add(rooms[j]);
                        rooms[j].Neighbours.Add(rooms[i]);
                    }
                }
            }
        }

        private bool CanDoRow(in int h, in Map map, Room roomA, Room roomB)
        {
            if (roomA.RightBottom.y >= roomB.LeftTop.y) (roomA, roomB) = (roomB, roomA);
            for (var w = roomA.RightBottom.y; w < roomB.LeftTop.y; w++)
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
            for (var h = roomA.RightBottom.x; h < roomB.LeftTop.x; h++)
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