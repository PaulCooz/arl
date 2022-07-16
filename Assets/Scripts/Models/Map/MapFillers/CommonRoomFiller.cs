using Libs;
using SettingObjects;
using UnityEngine;
using Random = System.Random;

namespace Models.Map.MapFillers
{
    public class CommonRoomFiller : IRoomFiller
    {
        private readonly Random _random;
        private readonly MapSettings.RoomFiller _setting;

        private Vector2Int? _exitPosition;
        private ushort _roomsFilled;

        public CommonRoomFiller(in MapSettings.RoomFiller setting, in int seed)
        {
            _random = new Random(seed);
            _roomsFilled = 0;
            _setting = setting;
        }

        public void Fill(in ushort[,] map, in Room room)
        {
            _exitPosition = null;
            for (var i = room.MinX; i < room.MaxX; i++)
            {
                for (var j = room.MinY; j < room.MaxY; j++)
                {
                    if (_roomsFilled > 0 && _random.Chance(_setting.enemyChance))
                    {
                        map[i, j].Add(TileType.Enemy);
                    }

                    if (!_exitPosition.HasValue || _random.Chance(_setting.exitChance)) _exitPosition = new Vector2Int(i, j);
                }
            }

            _roomsFilled++;
        }

        public void Conclude(in ushort[,] map)
        {
            if (_exitPosition == null)
            {
                Debug.LogWarning("no exit position");
                return;
            }

            map[_exitPosition.Value.x, _exitPosition.Value.y].Add(TileType.Exit);
        }
    }
}
