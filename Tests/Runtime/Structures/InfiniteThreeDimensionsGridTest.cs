using NUnit.Framework;
using UniCorn.Structures;
using UnityEngine;

namespace UniCorn.Tests.Structures
{
    public class InfiniteThreeDimensionsGridTest
    {
        private readonly Vector3 _cellSize = Vector3.one * 2;
        private static readonly int _batchSize = 16;

        private readonly Vector3Int _batchEntryPosition = Vector3Int.zero;
        private readonly Vector3Int _inBatchPosition = Vector3Int.one;
        private readonly Vector3Int _batchEndPosition = Vector3Int.one * (_batchSize - 1);

        private const string WORD_1 = "drftgy";
        private const string WORD_2 = "CYUhOIOI";
        private const string WORD_3 = "d16ez5d";
        private const string WORD_4 = "YIUdds%9";

        private InfiniteThreeDimensionsGrid<string> _grid;

        [SetUp]
        public void Setup()
        {
            _grid = new InfiniteThreeDimensionsGrid<string>(_cellSize, _batchSize);
        }

        [Test]
        public void ConstructorParameters()
        {
            Assert.AreEqual(_cellSize, _grid.CellSize);
            Assert.AreEqual(_batchSize, _grid.BatchSize);
        }

        [Test]
        public void PositionMappingInBatch()
        {
            Assert.AreEqual(_batchEntryPosition, _grid.GetPositionInBatch(_batchEntryPosition));
            Assert.AreEqual(_inBatchPosition, _grid.GetPositionInBatch(_inBatchPosition));
            Assert.AreEqual(_batchEndPosition, _grid.GetPositionInBatch(_batchEndPosition));

            Assert.AreEqual(_batchEndPosition, _grid.GetPositionInBatch(_inBatchPosition * -1));
            Assert.AreEqual(_inBatchPosition, _grid.GetPositionInBatch(_batchEndPosition * -1));

            Assert.AreEqual(_batchEntryPosition, _grid.GetPositionInBatch(_batchEntryPosition + Vector3Int.one * _batchSize));
            Assert.AreEqual(_inBatchPosition, _grid.GetPositionInBatch(_inBatchPosition + Vector3Int.one * _batchSize));
            Assert.AreEqual(_batchEndPosition, _grid.GetPositionInBatch(_batchEndPosition + Vector3Int.one * _batchSize));
        }

        [Test]
        public void PositionMapping()
        {
            Assert.AreEqual(Vector3Int.zero, _grid.GetBatchCoordinates(_batchEntryPosition));
            Assert.AreEqual(Vector3Int.zero, _grid.GetBatchCoordinates(_inBatchPosition));
            Assert.AreEqual(Vector3Int.zero, _grid.GetBatchCoordinates(_batchEndPosition));
        }

        [Test]
        public void SingleBatch()
        {
            Assert.IsNull(_grid.Get(_batchEntryPosition));
            Assert.IsNull(_grid.Get(_inBatchPosition));
            Assert.IsNull(_grid.Get(_batchEndPosition));

            _grid.Set(WORD_1, _batchEntryPosition);
            Assert.AreSame(WORD_1, _grid.Get(_batchEntryPosition));

            _grid.Set(WORD_2, _inBatchPosition);
            Assert.AreSame(WORD_2, _grid.Get(_inBatchPosition));

            _grid.Set(WORD_3, _batchEndPosition);
            Assert.AreSame(WORD_3, _grid.Get(_batchEndPosition));
        }

        [Test]
        public void PositionInWorld()
        {
            Assert.AreEqual(Vector3.zero, _grid.GetPositionInWorld(Vector3Int.zero));
            Assert.AreEqual(_cellSize, _grid.GetPositionInWorld(Vector3Int.one));
            Assert.AreEqual(-_cellSize, -_grid.GetPositionInWorld(Vector3Int.one));
        }
    }
}
