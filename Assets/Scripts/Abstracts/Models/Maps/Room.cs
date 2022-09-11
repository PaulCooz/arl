using System.Collections.Generic;
using UnityEngine;

namespace Abstracts.Models.Maps
{
    public class Room
    {
        public List<Room> Neighbours { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public Vector2Int LeftTop { get; private set; }
        public Vector2Int RightBottom { get; private set; }

        public bool IsImpasse => Neighbours.Count == 1;

        public Room(int height, int width, Vector2Int leftTop, Vector2Int rightBottom)
        {
            Height = height;
            Width = width;
            LeftTop = leftTop;
            RightBottom = rightBottom;
            Neighbours = new List<Room>();
        }

        public Room(int height, int width, int minX, int maxX, int minY, int maxY)
        {
            Height = height;
            Width = width;
            LeftTop = new Vector2Int(minX, minY);
            RightBottom = new Vector2Int(maxX, maxY);
            Neighbours = new List<Room>();
        }
    }
}