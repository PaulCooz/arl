using System.Collections.Generic;
using Common;
using Common.Configs;
using Common.Keys;
using Common.Storages.Preferences;
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
        private MapData startData;
        [SerializeField]
        private MapDrawer mapDrawer;
        [SerializeField]
        private GameMaster gameMaster;

        [SerializeField]
        private UnityEvent beforeFirstLevel;

        private void Start()
        {
            beforeFirstLevel.Invoke();

            ReadMapData();
            NextLevel();

            GameMaster.OnLevelDone += OnLevelDone;
        }

        private void ReadMapData()
        {
            _mapData = startData;
        }

        private void NextLevel()
        {
            _mapData.seed = Preference.Game.CurrentLevel;
            mapDrawer.Clear(CreateNewMap);
        }

        private void CreateNewMap()
        {
            var array = new List<Entities>[_mapData.height, _mapData.width];
            for (var i = 0; i < _mapData.height; i++)
            for (var j = 0; j < _mapData.width; j++)
            {
                array[i, j] = new List<Entities>();
            }

            _mapGenerator = new MapGenerator();
            var roomFiller = new RoomFiller(array);

            mapDrawer.Draw(_mapGenerator.GetNextMap(array, _mapData, roomFiller), _mapData.seed);

            this.WaitFrames(1, gameMaster.LevelCreated);
        }

        private void OnLevelDone()
        {
            Preference.Game.CurrentLevel++;
            NextLevel();
        }
    }
}