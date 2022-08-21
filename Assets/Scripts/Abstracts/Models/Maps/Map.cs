using System.Collections.Generic;
using UnityEngine;

namespace Abstracts.Models.Maps
{
    public class Map
    {
        private readonly List<Entities>[,] array;

        public Room FirstRoom { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        public List<Entities> this[int i, int j]
        {
            get => array[i, j];
            set => array[i, j] = value;
        }

        public List<Entities> this[Vector2Int c]
        {
            get => array[c.x, c.y];
            set => array[c.x, c.y] = value;
        }

        public Map(List<Entities>[,] array, Room firstRoom, int height, int width)
        {
            this.array = array;
            FirstRoom = firstRoom;
            Height = height;
            Width = width;
        }
    }
}