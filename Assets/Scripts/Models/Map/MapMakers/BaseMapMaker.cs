using System.Text;
using Models.Map.MapFillers;
using UnityEngine;

namespace Models.Map.MapMakers
{
    public abstract class BaseMapMaker : IMapMaker
    {
        protected static readonly Vector2Int[] Steps =
        {
            new(1, 0), new(-1, 0),
            new(0, 1), new(0, -1)
        };
        protected static readonly Vector2Int[] BlockSteps =
        {
            new(1, 0), new(-1, 0),
            new(0, 1), new(0, -1),
            new(-1, -1), new(-1, 1),
            new(1, 1), new(1, -1)
        };

        protected readonly ushort[,] Map;
        protected readonly int Width;
        protected readonly int Height;

        protected BaseMapMaker(int width, int height)
        {
            Width = width;
            Height = height;
            Map = new ushort[width, height];

            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    Map[i, j] = (int) TileType.Empty;
                }
            }
        }

        public abstract ushort[,] GetMap(in Vector2Int startPos, in IRoomFiller roomFiller, in int seed);

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    sb.Append(Map[i, j].Contains(TileType.Road) ? "██" : "  ");
                }

                sb.Append('\n');
            }

            return sb.ToString();
        }
    }
}
