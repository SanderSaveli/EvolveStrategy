using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TileSystem
{
    public class TerrainTilemap : MonoBehaviour
    {
        private Dictionary<Vector2Int, TerrainCell> _terrainTilemap;
        [SerializeField] private Tilemap _baseTilemap;
        private void Awake()
        {
            _baseTilemap = GetComponent<Tilemap>();
            InitializeTerrainMap();
        }

        public bool ContainTile(Vector3 worldCoordinate)
        {
            return ContainTile(
                GetCoordinateFromWorldPosition(worldCoordinate));
        }

        public bool ContainTile(Vector2Int gridPosition) => _terrainTilemap.ContainsKey(gridPosition);

        public TerrainCell GetTile(Vector3 worldPosition)
        {
            return GetTile(
                GetCoordinateFromWorldPosition(worldPosition));
        }
        public TerrainCell GetTile(Vector2Int gridPosition) => _terrainTilemap[gridPosition];

        public List<TerrainCell> GetCellNeighbors(TerrainCell terrainCell)
        {
            List<TerrainCell> neighbors = new();
            Vector2Int cellCoordinate = GetCoordinateFromWorldPosition(terrainCell.transform.position);
            List<Vector2Int> neighdorsCoordinate =
                HexMetrics.GetCoordinatesOfNeighboringCells(cellCoordinate);
            foreach (Vector2Int neighborCoordinate in neighdorsCoordinate)
            {
                if (_terrainTilemap.TryGetValue(neighborCoordinate, out TerrainCell cell))
                {
                    neighbors.Add(cell);
                }
            }
            return neighbors;
        }

        public bool isCellsNeighbours(TerrainCell a, TerrainCell b)
        {
            return GetCellNeighbors(a).Contains(b);
        }
        public Vector2Int GetCoordinateFromWorldPosition(Vector3 worldPosition)
        {
            worldPosition.z = _baseTilemap.transform.position.z;
            Vector3Int gridPosition = _baseTilemap.WorldToCell(worldPosition);
            return new Vector2Int(gridPosition.x, gridPosition.y);
        }

        public List<TerrainCell> GetAllCells()
        {
            List<TerrainCell> cells = new();
            foreach (KeyValuePair<Vector2Int, TerrainCell> pair in _terrainTilemap)
            {
                cells.Add(pair.Value);
            }
            return cells;
        }

        public Vector3Int GetCellCoordinate(TerrainCell cell)
        {
            return (Vector3Int)GetCoordinateFromWorldPosition(cell.transform.position);
        }

        public List<TerrainCell> GetStartCells(AcktorList acktor) 
        {
            List<TerrainCell> cells = new();
            foreach (KeyValuePair<Vector2Int, TerrainCell> pair in _terrainTilemap)
            {
                if (pair.Value.startOwner == acktor) 
                {
                    cells.Add(pair.Value);
                }
            }
            return cells;
        }

        private void InitializeTerrainMap()
        {
            _terrainTilemap = new();
            for (int i = 0; i < transform.childCount; i++)
            {
                AddToTilemapIfObjectIsCell(transform.GetChild(i));
            }

            foreach (TerrainCell cell in _terrainTilemap.Values)
            {
                if (cell.region == null)
                {
                    CreateRegionForCell(cell);
                }
            }
        }

        private void AddToTilemapIfObjectIsCell(Transform objectTransform)
        {
            if (objectTransform.TryGetComponent(out TerrainCell currentCell))
            {
                if (!_terrainTilemap.ContainsValue(currentCell))
                {
                    _terrainTilemap.Add(
                        GetCoordinateFromWorldPosition(currentCell.transform.position),
                        currentCell);
                }
                else
                {
                    throw new Exception("You already have tile in coordinate: " +
                        GetCoordinateFromWorldPosition(currentCell.transform.position));
                }
            }
        }

        private Region CreateRegionForCell(TerrainCell cell)
        {
            Region newRegion = new(DefineAllCellsForNewRegion(cell));
            newRegion.HideCellsInfo();
            return newRegion;
        }

        private List<TerrainCell> DefineAllCellsForNewRegion(TerrainCell startCell)
        {
            List<TerrainCell> regionCells = new();
            Queue<TerrainCell> cellToAnalyz = new();
            cellToAnalyz.Enqueue(startCell);

            while (cellToAnalyz.TryDequeue(out TerrainCell currentCell))
            {
                if (!regionCells.Contains(currentCell))
                {
                    regionCells.Add(currentCell);
                }
                List<TerrainCell> neighborhoodCells =
                    GetCellNeighbors(currentCell);
                foreach (TerrainCell neighbour in neighborhoodCells)
                {
                    if (!regionCells.Contains(neighbour) && neighbour.cellType == startCell.cellType)
                    {
                        cellToAnalyz.Enqueue(neighbour);
                    }
                }
            }

            return regionCells;
        }
    }

}
