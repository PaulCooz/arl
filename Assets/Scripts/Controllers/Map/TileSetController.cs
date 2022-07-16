using Controllers.Enemies.Slime;
using Controllers.Game;
using Controllers.Units;
using Controllers.Units.Enemies.Slime;
using Models.Map;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

namespace Controllers.Map
{
    public class TileSetController : MonoBehaviour
    {
        [SerializeField]
        private GameSceneBehaviours sceneBehaviours;

        [SerializeField]
        private Tilemap tilemapBottom;
        [SerializeField]
        private Tilemap tilemapMid;

        [SerializeField]
        private TileBase ground;
        [SerializeField]
        private TileBase wall;
        [SerializeField]
        private TileBase exit;

        [SerializeField]
        private SlimeUnitController slimePrefab;
        [SerializeField]
        private ShopMasterController shopPrefab;

        public void SetAll(in ushort[,] map, in float offsetX, in float offsetY, in Random mapRandom)
        {
            tilemapBottom.ClearAllTiles();
            tilemapMid.ClearAllTiles();

            var width = map.GetLength(0);
            var height = map.GetLength(1);
            for (var i = -1; i < width + 1; i++)
            {
                for (var j = -1; j < height + 1; j++)
                {
                    if (i == -1 || j == -1 || i == width || j == height)
                    {
                        tilemapMid.SetTile(new Vector3Int(i, j), wall);
                        tilemapBottom.SetTile(new Vector3Int(i, j), ground);
                        continue;
                    }

                    SetTile(map, offsetX, offsetY, mapRandom, i, j);
                }
            }
        }

        private void SetTile(in ushort[,] map, in float offsetX, in float offsetY, in Random mapRandom, in int i, in int j)
        {
            if (map[i, j].Contains(TileType.Enemy))
            {
                var slime = Instantiate(slimePrefab, tilemapMid.transform);
                slime.Init(sceneBehaviours);
                slime.transform.position = new Vector3(i + offsetX, j + offsetY);
            }

            if (map[i, j].Contains(TileType.Shop))
            {
                var shop = Instantiate(shopPrefab, tilemapMid.transform);
                shop.Init(sceneBehaviours);
                shop.transform.position = new Vector3(i + offsetX, j + offsetY);
            }

            if (map[i, j].Contains(TileType.Road))
            {
                tilemapBottom.SetTile(new Vector3Int(i, j), ground);
            }

            if (map[i, j].Contains(TileType.Exit))
            {
                tilemapMid.SetTile(new Vector3Int(i, j), exit);
            }

            if (map[i, j].Contains(TileType.Wall) || !map[i, j].Contains(TileType.Road))
            {
                tilemapMid.SetTile(new Vector3Int(i, j), wall);
            }
        }
    }
}
