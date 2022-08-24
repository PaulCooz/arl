using System.Collections.Generic;
using Abstracts.Models.Maps;
using UnityEngine;

namespace Realisations.Models.Maps
{
    public class MapManager : MonoBehaviour
    {
        private MapGenerator _mapGenerator;

        [SerializeField]
        private MapFiller mapFiller;
        [SerializeField]
        private MapData mapData;

        private void Start()
        {
            var array = new List<Entities>[mapData.height, mapData.width];
            for (var i = 0; i < mapData.height; i++)
            for (var j = 0; j < mapData.width; j++)
            {
                array[i, j] = new List<Entities>();
            }

            _mapGenerator = new MapGenerator();
            var roomFiller = new RoomFiller(array);

            mapFiller.Fill(_mapGenerator.GetNextMap(array, mapData, roomFiller));
        }
    }
}