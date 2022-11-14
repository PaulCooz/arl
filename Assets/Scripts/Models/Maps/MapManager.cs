using System.Collections.Generic;
using Common;
using Common.Storages.Preferences;
using Models.Maps.Abstracts;
using Models.Progressions;
using UnityEngine;
using UnityEngine.Events;

namespace Models.Maps
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField]
        private LevelProgressionObject progression;
        [SerializeField]
        private MapDrawer mapDrawer;
        [SerializeField]
        private GameMaster gameMaster;

        [SerializeField]
        private UnityEvent beforeFirstLevel;

        private void Start()
        {
            beforeFirstLevel.Invoke();

            NextLevel();
        }

        public void ClearAndCreateLevel()
        {
            Preference.Game.CurrentLevel++;
            NextLevel();
        }

        private void NextLevel()
        {
            mapDrawer.Clear(CreateNewMap);
        }

        private void CreateNewMap()
        {
            progression.GetData(Preference.Game.CurrentLevel, out var map, out var units, out var isBossFight);

            var array = new List<Entities>[map.height, map.width];

            for (var i = 0; i < map.height; i++)
            for (var j = 0; j < map.width; j++)
            {
                array[i, j] = new List<Entities>();
            }

            GetMapCreators(isBossFight, out var mapGenerator, out var roomFiller);
            roomFiller.Setup(array);

            var nextMap = mapGenerator.GetNextMap(array, map, roomFiller);
            mapDrawer.Draw(nextMap, map.seed, units);

            this.WaitFrames(1, gameMaster.CreatedNewLevel);
        }

        private void GetMapCreators(in bool isBossFight, out IMapGenerator generator, out IRoomFiller filler)
        {
            if (isBossFight)
            {
                generator = new MapBossGenerator();
                filler = new BossRoomFiller();
            }
            else
            {
                generator = new MapGenerator();
                filler = new RoomFiller();
            }
        }
    }
}