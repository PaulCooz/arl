using Common.Arrays;
using Models.CollisionTriggers;
using Models.Maps.Abstracts;
using Models.Unit;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Models.Maps
{
    public class MapDrawer : MonoBehaviour
    {
        private static readonly Vector3 PositionOffset = new(0.5f, 0.5f, 0);

        private bool _isFirstTime = true;

        [SerializeField]
        private UnitRoot player;
        [SerializeField]
        private GameMaster gameMaster;

        [SerializeField]
        private UnitRoot enemyPrefab;
        [SerializeField]
        private ExitCollisionTrigger exitTriggerPrefab;

        [SerializeField]
        private TileBase[] floorTiles;
        [SerializeField]
        private TileBase[] wallsTiles;
        [SerializeField]
        private TileBase[] exitTiles;
        [SerializeField]
        private Tilemap floorTilemap;
        [SerializeField]
        private Tilemap wallsTilemap;

        [SerializeField]
        private UnitsConfigHelper unitsConfig;

        public void Draw(in Map map, int seed)
        {
            var random = new System.Random(seed);
            for (var i = 0; i < map.Height; i++)
            for (var j = 0; j < map.Width; j++)
            {
                if (map[i, j].Contains(Entities.Floor))
                {
                    floorTilemap.SetTile(new Vector3Int(i, j), floorTiles.Random());
                }

                if (map[i, j].Contains(Entities.Wall))
                {
                    wallsTilemap.SetTile(new Vector3Int(i, j), wallsTiles.Random());
                }

                if (map[i, j].Contains(Entities.Enemy))
                {
                    var enemy = Instantiate(enemyPrefab, GetPosition(i, j), Quaternion.identity);
                    enemy.transform.SetParent(transform);
                    enemy.Unit.Name = unitsConfig.GetUnitName(random);
                    enemy.Initialization();
                }

                if (map[i, j].Contains(Entities.Player))
                {
                    player.transform.position = GetPosition(i, j);
                    if (_isFirstTime)
                    {
                        _isFirstTime = false;
                        player.Initialization();
                    }
                }

                if (map[i, j].Contains(Entities.Exit))
                {
                    var exit = Instantiate(exitTriggerPrefab, GetPosition(i, j), Quaternion.identity);
                    exit.Init(gameMaster);
                    exit.transform.SetParent(transform);

                    floorTilemap.SetTile(new Vector3Int(i, j), exitTiles.Random());
                }
            }
        }

        public void Clear()
        {
            floorTilemap.ClearAllTiles();
            wallsTilemap.ClearAllTiles();
        }

        private Vector3 GetPosition(in int i, in int j)
        {
            return new Vector3(i, j, 0) + PositionOffset;
        }
    }
}