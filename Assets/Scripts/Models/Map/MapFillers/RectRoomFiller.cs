using Libs;
using SettingObjects;
using UnityEngine;
using Random = System.Random;

namespace Models.Map.MapFillers
{
    public class RectRoomFiller : IRoomFiller
    {
        private readonly Random _random;
        private readonly MapSettings.RoomFiller _setting;

        private Vector2Int? _exitPosition;
        private Vector2Int? _shopPosition;
        private ushort _roomsFilled;
        private bool _isSetShop;

        public RectRoomFiller(in MapSettings.RoomFiller setting, in int seed)
        {
            _random = new Random(seed);
            _setting = setting;
            _roomsFilled = 0;
            _isSetShop = false;
        }

        public void Fill(in ushort[,] map, in Room room)
        {
            _exitPosition = null;
            for (var i = room.MinX; i < room.MaxX; i++)
            {
                for (var j = room.MinY; j < room.MaxY; j++)
                {
                    if (!_isSetShop && _roomsFilled > 0 && (!_shopPosition.HasValue || _random.Chance(_setting.shopChance)))
                    {
                        _shopPosition = new Vector2Int(i, j);
                    }
                    else if (!_exitPosition.HasValue || _random.Chance(_setting.exitChance))
                    {
                        _exitPosition = new Vector2Int(i, j);
                    }
                }
            }

            if (_shopPosition.HasValue)
            {
                map[_shopPosition.Value.x, _shopPosition.Value.y].Add(TileType.Shop);
                _isSetShop = true;
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
