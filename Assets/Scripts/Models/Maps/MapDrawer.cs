using System;
using System.Collections.Generic;
using Common;
using Common.Arrays;
using Common.Configs;
using Models.CollisionTriggers;
using Models.Maps.Abstracts;
using Models.Unit;
using UnityEngine;
using UnityEngine.Events;
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
        private Pair<string, UnitRoot>[] enemies;
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
                    var unitName = unitsConfig.GetUnitConfig(random);
                    var enemy = Instantiate(EnemyPrefab(unitName), GetPosition(i, j), Quaternion.identity);

                    enemy.transform.SetParent(transform);
                    enemy.Unit.UnitConfig = unitName;
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

        private UnitRoot EnemyPrefab(UnitConfigObject config)
        {
            var key = config.prefabName;
            foreach (var p in enemies)
            {
                if (p.key == key) return p.value;
            }

            throw new NullReferenceException();
        }

        public void Clear(UnityAction onComplete)
        {
            StartCoroutine(ClearCoroutine(onComplete));
        }

        private IEnumerator<WaitForSeconds> ClearCoroutine(UnityAction onComplete)
        {
            yield return null;

            floorTilemap.ClearAllTiles();
            wallsTilemap.ClearAllTiles();

            yield return null;

            onComplete.Invoke();
        }

        private Vector3 GetPosition(in int i, in int j)
        {
            return new Vector3(i, j, 0) + PositionOffset;
        }
    }
}