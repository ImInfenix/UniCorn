using System.Collections.Generic;
using UnityEngine;

namespace UniCorn.Structures
{
    public class InfiniteThreeDimensionsGrid<T> where T : class
    {
        private readonly Vector3 _cellSize;
        private readonly int _batchSize;

        private Dictionary<Vector3Int, T[,,]> _batchesMap;

        public Vector3 CellSize => _cellSize;
        public int BatchSize => _batchSize;

        public InfiniteThreeDimensionsGrid(Vector3 cellSize, int batchSize = 16)
        {
            _cellSize = cellSize;
            _batchSize = batchSize;
            _batchesMap = new Dictionary<Vector3Int, T[,,]>();
        }

        public T Get(Vector3Int position)
        {
            if (!_batchesMap.TryGetValue(GetBatchCoordinates(position), out T[,,] batch))
            {
                return null;
            }

            Vector3Int positionInBatch = GetPositionInBatch(position);

            return batch[positionInBatch.x % _batchSize, positionInBatch.y % _batchSize, positionInBatch.z % _batchSize];
        }

        public Vector3 GetPositionInWorld(Vector3Int positionInTheGrid)
        {
            return new Vector3(positionInTheGrid.x * _cellSize.x, positionInTheGrid.x * _cellSize.y, positionInTheGrid.x * _cellSize.z);
        }

        public void Set(T value, Vector3Int position)
        {
            Vector3Int batchCoordinates = GetBatchCoordinates(position);

            if (!_batchesMap.TryGetValue(batchCoordinates, out T[,,] batch))
            {
                batch = new T[_batchSize, _batchSize, _batchSize];
                _batchesMap[batchCoordinates] = batch;
            }

            Vector3Int positionInBatch = GetPositionInBatch(position);

            batch[positionInBatch.x, positionInBatch.y, positionInBatch.z] = value;
        }

        public Vector3Int GetPositionInBatch(Vector3Int positionInGrid)
        {
            int GetPositionInBatchSingleAxis(int position)
            {
                int result = position % _batchSize;

                if (result < 0)
                {
                    result += _batchSize;
                }

                return result;
            }

            return new Vector3Int(GetPositionInBatchSingleAxis(positionInGrid.x), GetPositionInBatchSingleAxis(positionInGrid.y), GetPositionInBatchSingleAxis(positionInGrid.z));
        }

        public Vector3Int GetBatchCoordinates(Vector3Int positionInTheGrid)
        {
            return positionInTheGrid / _batchSize;
        }
    }
}
