using System;
using System.Collections.Generic;
using Libs;
using Models.Map.MapFillers;
using SettingObjects;
using UnityEngine;
using Random = System.Random;

namespace Models.Map.MapMakers
{
    public class CommonMapMaker : BaseMapMaker
    {
        private readonly Stack<Vector2Int> _positions;
        private readonly MapSettings.MapMaker _settings;

        private Vector2Int _startPos;

        public CommonMapMaker(in MapSettings.MapMaker settings, int width, int height) : base(width, height)
        {
            _positions = new Stack<Vector2Int>();
            _settings = settings;
        }

        public override ushort[,] GetMap(in Vector2Int startPos, in IRoomFiller roomFiller, in int seed)
        {
            _startPos = startPos;
            var rand = new Random(seed);

            SetRect
            (
                new Vector2Int(0, 0),
                _startPos,
                rand.Next(_settings.startRoomWidth),
                rand.Next(_settings.startRoomHeight),
                rand,
                roomFiller
            );

            while (!_positions.IsEmpty())
            {
                NextStep(rand, roomFiller);
            }

            Conclude(roomFiller);

            return Map;
        }

        private void Conclude(in IRoomFiller roomFiller)
        {
            for (var i = 0; HasDeadEnds() && i < _settings.clearDeadEndMaxIterations; i++)
            {
                ClearDeadEnds();
            }

            var copy = new ushort[Width, Height];
            Array.Copy(Map, copy, Map.Length);

            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    if (copy[i, j].Contains(TileType.Road)) continue;

                    var current = new Vector2Int(i, j);
                    var countAround = CountAround
                    (
                        current,
                        adjacent => !copy[adjacent.x, adjacent.y].Contains(TileType.Road),
                        false
                    );

                    if (countAround > 0)
                    {
                        Map[i, j].Add(TileType.Wall);
                        Map[i, j].Add(TileType.Road);
                    }
                }
            }

            roomFiller.Conclude(Map);
        }

        private bool HasDeadEnds()
        {
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    if (Map[i, j].Contains(TileType.Road) && IsDeadEnd(new Vector2Int(i, j)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void ClearDeadEnds()
        {
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    if (IsDeadEnd(new Vector2Int(i, j)))
                    {
                        Map[i, j] = (int) TileType.Empty;
                    }
                }
            }
        }

        private void NextStep(in Random rand, in IRoomFiller roomFiller)
        {
            Steps.RandomShuffle(rand);

            var current = _positions.Pop();
            var isRoomCheck = true;

            foreach (var direction in Steps)
            {
                var next = current + direction;

                if (!IsInRect(next) || !IsDeadEnd(current, next)) continue;

                for (var w = rand.Next(_settings.roomWidth); isRoomCheck && w > _settings.roomWidth.Min; w--)
                {
                    for (var h = rand.Next(_settings.roomHeight); isRoomCheck && h > _settings.roomHeight.Min; h--)
                    {
                        if (!CanRect(direction, next, w, h)) continue;

                        SetRect(direction, next, w, h, rand, roomFiller);
                        isRoomCheck = false;
                    }
                }

                if (!isRoomCheck && (direction.x < 0 || direction.y < 0))
                {
                    SetTile(next + direction);
                }

                SetTile(next);
            }
        }

        private bool CanRect(in Vector2Int direction, Vector2Int position, in int width, in int height)
        {
            GetRoomCenter(ref position, direction, width, height);

            var room = new Room(position, width, height);
            for (var i = room.MinX; i <= room.MaxX; i++)
            {
                for (var j = room.MinY; j <= room.MaxY; j++)
                {
                    var next = new Vector2Int(i, j);
                    var countAround = CountAround
                    (
                        next,
                        adjacent => !Map[adjacent.x, adjacent.y].Contains(TileType.Road),
                        false
                    );

                    if (!IsInRect(next) || Map[i, j].Contains(TileType.Road) || countAround > 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void GetRoomCenter(ref Vector2Int fromPosition, in Vector2Int direction, in int width, in int height)
        {
            fromPosition += new Vector2Int(direction.x * ((width | 1) + 1) / 2, direction.y * ((height | 1) + 1) / 2);
        }

        private void SetRect(in Vector2Int direction, Vector2Int position, in int width, in int height, in Random rand,
            in IRoomFiller roomFiller)
        {
            GetRoomCenter(ref position, direction, width, height);

            var toPush = new List<Vector2Int>();
            var room = new Room(position, width, height);
            for (var i = room.MinX; i <= room.MaxX; i++)
            {
                for (var j = room.MinY; j <= room.MaxY; j++)
                {
                    toPush.Add(new Vector2Int(i, j));
                }
            }

            toPush.RandomShuffle(rand);
            foreach (var pos in toPush)
            {
                SetTile(pos);
            }

            roomFiller.Fill(Map, room);
        }

        private void SetTile(in Vector2Int next)
        {
            Map[next.x, next.y].Add(TileType.Road);
            _positions.Push(next);
        }

        private bool IsDeadEnd(Vector2Int current, in Vector2Int next)
        {
            return CountAround(next, adjacent => IsOtherRoad(current, adjacent), false) < 1;
        }

        private bool IsOtherRoad(Vector2Int current, Vector2Int adjacent)
        {
            return !Map[adjacent.x, adjacent.y].Contains(TileType.Road) || adjacent == current ||
                   Distance(current, adjacent) < 2;
        }

        private bool IsDeadEnd(in Vector2Int current)
        {
            return CountAround(current, adjacent => !Map[adjacent.x, adjacent.y].Contains(TileType.Road), true) < 2;
        }

        private int CountAround(in Vector2Int position, in Func<Vector2Int, bool> isSkipAdjacent, in bool isCommonSteps)
        {
            var result = 0;
            foreach (var step in isCommonSteps ? Steps : BlockSteps)
            {
                var adjacent = position + step;

                if (!IsInRect(adjacent) || isSkipAdjacent(adjacent))
                {
                    continue;
                }

                result++;
            }

            return result;
        }

        private int Distance(in Vector2Int left, in Vector2Int right)
        {
            return Math.Abs(left.x - right.x) + Math.Abs(left.y - right.y);
        }

        private bool IsInRect(in Vector2Int next)
        {
            return 0 <= next.x && next.x < Width && 0 <= next.y && next.y < Height;
        }
    }
}
