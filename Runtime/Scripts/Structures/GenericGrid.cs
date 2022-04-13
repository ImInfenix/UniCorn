using System;
using UnityEngine;

namespace UniCorn.Structures
{
    public class GenericGrid<T>
    {
        public int Width { get; }
        public int Height { get; }
        private T[,] GridContent { get; }

        public T this[Vector2Int position]
        {
            get => this[position.x, position.y];
            set => this[position.x, position.y] = value;
        }

        public T this[int x, int y]
        {
            get => GridContent[x, y];
            set => GridContent[x, y] = value;
        }

        public GenericGrid(int width, int height)
        {
            if (width <= 0)
            {
                throw new ArgumentException($"The parameter {width} must be higher than 0");
            }
            
            if (height <= 0)
            {
                throw new ArgumentException($"The parameter {height} must be higher than 0");
            }
            
            Width = width;
            Height = height;

            GridContent = new T[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    GridContent[i, j] = default;
                }
            }
        }

        public bool IsPositionInGrid(Vector2Int position) => IsPositionInGrid(position.x, position.y);
        public bool IsPositionInGrid(int x, int y) => x >= 0 && x < Width && y >= 0 && y < Height;
    }
}