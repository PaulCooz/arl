using System.Collections.Generic;
using Common.Keys;
using Common.Storages.Configs;
using Models.Maps.Abstracts;
using UnityEngine;
using UnityEngine.Events;

namespace Models.Maps
{
    public class MapManager : MonoBehaviour
    {
        private MapGenerator _mapGenerator;
        private MapData _mapData;

        [SerializeField]
        private MapDrawer mapDrawer;

        [SerializeField]
        private UnityEvent beforeFirstLevel;

        private void Start()
        {
            beforeFirstLevel.Invoke();

            ReadMapData();
            NextLevel();
        }

        private void ReadMapData()
        {
            _mapData = Config.Get<MapData>(ConfigKey.MapGeneration);
        }

        public void NextLevel()
        {
            mapDrawer.Clear();
            _mapData.seed++;

            var array = new List<Entities>[_mapData.height, _mapData.width];
            for (var i = 0; i < _mapData.height; i++)
            for (var j = 0; j < _mapData.width; j++)
            {
                array[i, j] = new List<Entities>();
            }

            _mapGenerator = new MapGenerator();
            var roomFiller = new RoomFiller(array);

            mapDrawer.Draw(_mapGenerator.GetNextMap(array, _mapData, roomFiller), _mapData.seed);
        }
    }
}