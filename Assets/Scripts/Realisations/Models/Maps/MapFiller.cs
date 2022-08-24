using Abstracts.Models.Maps;
using Common.Arrays;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Realisations.Models.Maps
{
    public class MapFiller : MonoBehaviour
    {
        private static readonly Vector3 PositionOffset = new(0.5f, 0.5f, 0);

        [SerializeField]
        private Transform playerTransform;

        [SerializeField]
        private Transform enemyPrefab;

        [SerializeField]
        private Tile[] floorTiles;
        [SerializeField]
        private Tile[] wallsTiles;
        [SerializeField]
        private Tilemap floorTilemap;
        [SerializeField]
        private Tilemap wallsTilemap;

        public void Fill(in Map map)
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
                    var enemy = Instantiate(enemyPrefab, new Vector3(i, j, 0) + PositionOffset, Quaternion.identity);
                    enemy.SetParent(transform);
                }

                if (map[i, j].Contains(Entities.Player))
                {
                    playerTransform.position = new Vector3(i, j, 0) + PositionOffset;
                }
            }
        }
    }
}