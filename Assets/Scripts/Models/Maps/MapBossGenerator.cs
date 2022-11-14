using System;
using System.Collections.Generic;
using Models.Maps.Abstracts;

namespace Models.Maps
{
    public class MapBossGenerator : IMapGenerator
    {
        private List<Entities>[,] _map;
        private MapData _data;
        private IRoomFiller _filler;
        private Random _rand;

        public Map GetNextMap(in List<Entities>[,] array, in MapData mapData, in IRoomFiller roomFiller)
        {
            _map = array;
            _data = mapData;
            _filler = roomFiller;
            _rand = new Random(mapData.seed);

            var startRoom = new Room
            (
                mapData.height,
                mapData.width,
                0,
                mapData.height,
                0,
                mapData.width
            );

            roomFiller.FillCommon(startRoom, _rand);

            return new Map(array, startRoom, mapData.height, mapData.width);
        }
    }
}