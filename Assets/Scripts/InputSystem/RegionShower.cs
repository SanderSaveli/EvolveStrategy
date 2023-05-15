using TileSystem;
using UnityEngine;

namespace UISystem 
{ 
    public class RegionShower
    {
        private TerrainTilemap _terrainTilemap;
        private GameStateManager _gameStateManager;

        private Region _previousRegion = null;

        public RegionShower() 
        {
            _terrainTilemap = Object.FindObjectOfType<TerrainTilemap>();
            _gameStateManager = ServiceLocator.Get<GameStateManager>();

            InputManager.instance.OnCursorPositionChanged += ShowRegion;
        }

        private void ShowRegion(Vector3 position)
        {
            if (_gameStateManager.currentState == GameStates.Battle)
            {
                if (_terrainTilemap.ContainTile(position))
                {
                    Region newRegion = _terrainTilemap.GetTile(position).region;
                    if (newRegion.isFade && newRegion != _previousRegion)
                    {
                        newRegion.ShowCellsInfo();
                        if (_previousRegion != null)
                            _previousRegion.HideCellsInfo();
                        _previousRegion = newRegion;
                    }
                }
                else
                {
                    if (_previousRegion != null)
                    {
                        _previousRegion.HideCellsInfo();
                        _previousRegion = null;
                    }
                }
            }

        }
    }
}

