using System.Collections.Generic;
using UnityEngine;

namespace Models.Maps.Abstracts
{
    public class Map
    {
        private readonly List<Entities>[,] _array;

        public Room FirstRoom { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        public List<Entities> this[int i, int j]
        {
            get => _array[i, j];
            set => _array[i, j] = value;
        }

        public List<Entities> this[Vector2Int c]
        {
            get => _array[c.x, c.y];
            set => _array[c.x, c.y] = value;
        }

        public Map(List<Entities>[,] array, Room firstRoom, int height, int width)
        {
            this._array = array;
            FirstRoom = firstRoom;
            Height = height;
            Width = width;
        }
    }
}