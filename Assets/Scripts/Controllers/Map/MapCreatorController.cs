using System.Collections;
using Models.Map.MapFillers;
using Models.Map.MapMakers;
using SettingObjects;
using UnityEngine;
using Random = System.Random;

namespace Controllers.Map
{
    public class MapCreatorController : MonoBehaviour
    {
        private const float OffsetX = 0.5f;
        private const float OffsetY = 0.5f;

        private int _mapNumber = 0;

        [SerializeField]
        private MapSettingObject mapSettingObject;
        [SerializeField]
        private TileSetController tileSetController;
        [SerializeField]
        private GameObject player;

        [SerializeField]
        private int mapSize;

        public Random Random { get; private set; }
        public int Seed { get; private set; }

        public void Awake()
        {
            NextMap();
        }

        public void MapDone()
        {
            StartCoroutine(SetNextLevel());
        }

        private IEnumerator SetNextLevel()
        {
            yield return null;

            NextMap();
        }

        private void NextMap()
        {
            Seed = _mapNumber;
            Random = new Random(Seed);

            var startPos = new Vector2Int(mapSize / 2, mapSize / 2);

            tileSetController.SetAll(CreateNewMap(_mapNumber, mapSize, startPos), OffsetX, OffsetY, Random);
            player.transform.position = new Vector3(startPos.x + OffsetX, startPos.y + OffsetY, 1);

            _mapNumber++;
        }

        private ushort[,] CreateNewMap(in int mapNumber, in int edge, in Vector2Int startPos)
        {
            var mapSettings = mapSettingObject.mapSettings;
            if (mapNumber % 4 == 0)
            {
                var mapMaker = new CommonMapMaker(mapSettings.mapMaker, edge, edge);
                return mapMaker.GetMap(startPos, new RectRoomFiller(mapSettings.roomFiller, Seed), Seed);
            }
            else
            {
                var mapMaker = new CommonMapMaker(mapSettings.mapMaker, edge, edge);
                return mapMaker.GetMap(startPos, new CommonRoomFiller(mapSettings.roomFiller, Seed), Seed);
            }
        }
    }
}