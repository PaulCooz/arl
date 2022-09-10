﻿using Abstracts.Models.Maps;
using Common.Arrays;
using Realisations.Models.CollisionTriggers;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Realisations.Models.Maps
{
    public class MapDrawer : MonoBehaviour
    {
        private static readonly Vector3 PositionOffset = new(0.5f, 0.5f, 0);

        [SerializeField]
        private Transform playerTransform;
        [SerializeField]
        private GameMaster gameMaster;

        [SerializeField]
        private Transform enemyPrefab;
        [SerializeField]
        private ExitCollisionTrigger exitTriggerPrefab;

        [SerializeField]
        private Tile[] floorTiles;
        [SerializeField]
        private Tile[] wallsTiles;
        [SerializeField]
        private Tile[] exitTiles;
        [SerializeField]
        private Tilemap floorTilemap;
        [SerializeField]
        private Tilemap wallsTilemap;

        public void Draw(in Map map)
        {
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
                    enemy.SetParent(transform);
                }

                if (map[i, j].Contains(Entities.Player))
                {
                    playerTransform.position = GetPosition(i, j);
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