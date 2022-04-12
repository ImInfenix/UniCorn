using System;
using NUnit.Framework;
using UniCorn.Structures;
using UnityEngine;

namespace UniCorn.Tests.Structures
{
    public class GridTest
    {
        private const int HEIGHT = 10;
        private const int WIDTH = 10;

        private Grid<int> _grid;

        [SetUp]
        public void Setup()
        {
            _grid = new Grid<int>(WIDTH, HEIGHT);
        }

        [Test]
        public void ConstructorParameters()
        {
            Assert.AreEqual(WIDTH, _grid.Width);
            Assert.AreEqual(HEIGHT, _grid.Height);
        }

        [Test]
        public void ConstructorExceptions()
        {
            ArgumentException widthException = null;
            ArgumentException heightException = null;

            try
            {
                var unusedGrid = new Grid<int>(0, 1);
            }
            catch (ArgumentException e)
            {
                widthException = e;
            }

            try
            {
                var unusedGrid = new Grid<int>(1, 0);
            }
            catch (ArgumentException e)
            {
                heightException = e;
            }

            Assert.IsNotNull(widthException);
            Assert.IsNotNull(heightException);
        }

        [Test]
        public void GetterDefaultValue()
        {
            Assert.AreEqual(_grid[0, 0], 0);
            Assert.AreEqual(_grid[Vector2Int.zero], 0);
        }

        [Test]
        public void GetterAndSetterModifiedValue()
        {
            _grid[1, 1] = 8;
            Assert.AreEqual(_grid[1, 1], 8);
            _grid[Vector2Int.zero] = 5;
            Assert.AreEqual(_grid[Vector2Int.zero], 5);
        }

        [Test]
        public void IsPositionInGrid()
        {
            Assert.IsTrue(_grid.IsPositionInGrid(0, 0));
            Assert.IsTrue(_grid.IsPositionInGrid(Vector2Int.zero));
            
            Assert.IsTrue(_grid.IsPositionInGrid(WIDTH - 1, 0));
            Assert.IsTrue(_grid.IsPositionInGrid(new Vector2Int(WIDTH - 1, 0)));
            
            Assert.IsTrue(_grid.IsPositionInGrid(0, HEIGHT - 1));
            Assert.IsTrue(_grid.IsPositionInGrid(new Vector2Int(0, HEIGHT - 1)));
            
            Assert.IsTrue(_grid.IsPositionInGrid(WIDTH - 1, HEIGHT - 1));
            Assert.IsTrue(_grid.IsPositionInGrid(new Vector2Int(WIDTH - 1, HEIGHT - 1)));
            
            Assert.IsTrue(_grid.IsPositionInGrid(0, 0));
            Assert.IsTrue(_grid.IsPositionInGrid(Vector2Int.zero));
            
            Assert.IsFalse(_grid.IsPositionInGrid(-1, -1));
            Assert.IsFalse(_grid.IsPositionInGrid(new Vector2Int(-1, -1)));
        }
    }
}
