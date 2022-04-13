using System;
using NUnit.Framework;
using UniCorn.Structures;
using UnityEngine;

namespace UniCorn.Tests.Structures
{
    public class GenericGridTest
    {
        private const int HEIGHT = 10;
        private const int WIDTH = 10;

        private GenericGrid<int> _genericGrid;

        [SetUp]
        public void Setup()
        {
            _genericGrid = new GenericGrid<int>(WIDTH, HEIGHT);
        }

        [Test]
        public void ConstructorParameters()
        {
            Assert.AreEqual(WIDTH, _genericGrid.Width);
            Assert.AreEqual(HEIGHT, _genericGrid.Height);
        }

        [Test]
        public void ConstructorExceptions()
        {
            ArgumentException widthException = null;
            ArgumentException heightException = null;

            try
            {
                var unusedGrid = new GenericGrid<int>(0, 1);
            }
            catch (ArgumentException e)
            {
                widthException = e;
            }

            try
            {
                var unusedGrid = new GenericGrid<int>(1, 0);
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
            Assert.AreEqual(_genericGrid[0, 0], 0);
            Assert.AreEqual(_genericGrid[Vector2Int.zero], 0);
        }

        [Test]
        public void GetterAndSetterModifiedValue()
        {
            _genericGrid[1, 1] = 8;
            Assert.AreEqual(_genericGrid[1, 1], 8);
            _genericGrid[Vector2Int.zero] = 5;
            Assert.AreEqual(_genericGrid[Vector2Int.zero], 5);
        }

        [Test]
        public void IsPositionInGrid()
        {
            Assert.IsTrue(_genericGrid.IsPositionInGrid(0, 0));
            Assert.IsTrue(_genericGrid.IsPositionInGrid(Vector2Int.zero));
            
            Assert.IsTrue(_genericGrid.IsPositionInGrid(WIDTH - 1, 0));
            Assert.IsTrue(_genericGrid.IsPositionInGrid(new Vector2Int(WIDTH - 1, 0)));
            
            Assert.IsTrue(_genericGrid.IsPositionInGrid(0, HEIGHT - 1));
            Assert.IsTrue(_genericGrid.IsPositionInGrid(new Vector2Int(0, HEIGHT - 1)));
            
            Assert.IsTrue(_genericGrid.IsPositionInGrid(WIDTH - 1, HEIGHT - 1));
            Assert.IsTrue(_genericGrid.IsPositionInGrid(new Vector2Int(WIDTH - 1, HEIGHT - 1)));
            
            Assert.IsTrue(_genericGrid.IsPositionInGrid(0, 0));
            Assert.IsTrue(_genericGrid.IsPositionInGrid(Vector2Int.zero));
            
            Assert.IsFalse(_genericGrid.IsPositionInGrid(-1, -1));
            Assert.IsFalse(_genericGrid.IsPositionInGrid(new Vector2Int(-1, -1)));
        }
    }
}