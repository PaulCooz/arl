﻿using System.Collections.Generic;
using Abstracts.Models.Maps;
using UnityEngine;
using UnityEngine.Events;

namespace Realisations.Models.Maps
{
    public class MapManager : MonoBehaviour
    {
        private MapGenerator _mapGenerator;

        private bool _isFirst = true;

        [SerializeField]
        private MapDrawer mapDrawer;
        [SerializeField]
        private MapData mapData;

        [SerializeField]
        private UnityEvent beforeFirstLevel;

        private void Start()
        {
            beforeFirstLevel.Invoke();
            NextLevel();
        }

        public void NextLevel()
        {
            if (!_isFirst) mapDrawer.Clear();
            mapData.seed++;

            var array = new List<Entities>[mapData.height, mapData.width];
            for (var i = 0; i < mapData.height; i++)
            for (var j = 0; j < mapData.width; j++)
            {
                array[i, j] = new List<Entities>();
            }

            _mapGenerator = new MapGenerator();
            var roomFiller = new RoomFiller(array);

            mapDrawer.Draw(_mapGenerator.GetNextMap(array, mapData, roomFiller), mapData.seed);

            _isFirst = false;
        }
    }
}