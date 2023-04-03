using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

namespace TileSystem
{
    public class Region: IPlayerChoosesNestCellHandler, INestDestroyed
    {
        private List<TerrainCell> _regionCells = new();

        public bool isNestInRegion;

        private bool isRegionControledPlayer;
        private GameObject _regionView;
        private RegionBorder _regionBoundes;
        private NestBuildView _buildView;

        private CellType _cellType;
        public bool isFade;

        public Region(List<TerrainCell> regionCells) 
        {
            _regionCells = regionCells;
            _regionView = new GameObject("RegionView");
            _regionView.transform.SetParent(GameObject.FindGameObjectWithTag("GUICanvas").transform, false);
            _regionView.AddComponent<RectTransform>();
            _cellType = regionCells[0].cellType;
            foreach(TerrainCell cell in _regionCells) 
            {
                cell.region = this;
                isNestInRegion |= cell.isNestBuilt;
                cell.OnOwnerChenge += NotifyIfRegionControlStatusChanged;
            }
            CreateRegionBorders();
            EventBus.Subscribe(this);
        }

        public List<TerrainCell> GetRegionCells() 
        {
            return _regionCells;
        }

        public void ShowCellsInfo() 
        {
            isFade = true;
            _regionBoundes.ShowBorders();
        }

        public void HideCellsInfo() 
        {
            isFade = true;
            _regionBoundes.HideBorders();
        }

        private void NotifyIfRegionControlStatusChanged(GameAcktor newOwner, TerrainCell cell) 
        {
            if (IsOnePlayerControlRegion()) 
            {
                if(cell.owner.acktorName == AcktorList.Player) 
                {
                    isRegionControledPlayer = true;
                }
                EventBus.RaiseEvent<IRegionOwnershipStatusChangedHandler>(it => it.RegionControledBySinglePlayer(this, cell.owner));
                if(!isNestInRegion) 
                {
                    cell.owner.OfferToBuildNest(this);
                }
            }
            else if (isRegionControledPlayer) 
            {
                HideNestBuildingViewForPlayer();
            }
        }
        private bool IsOnePlayerControlRegion() 
        {
            GameAcktor owner = _regionCells[0].owner;
            foreach(TerrainCell cell in _regionCells) 
            { 
                if(cell.owner != owner) 
                {
                    return false;
                }
            }
            return true;
        }
        public void ShowNestBuildingViewForPlayer()
        {
            GameObject viewPrefab = (GameObject)Resources.Load("ViewElements/BuildNestIcon");
            GameObject viewObject = Object.Instantiate(viewPrefab, _regionView.transform);
            viewObject.transform.position = CalculateCenter();
            _buildView = viewObject.GetComponent<NestBuildView>();
            _buildView.OnClick += PlayerClickedOnNestBuildButton;
        }

        private void HideNestBuildingViewForPlayer()
        {
            EventBus.RaiseEvent<IPlayerChoosesNestCellHandler>(it => it.EndState(this));
            Object.Destroy(_buildView.gameObject);
        }

        private void PlayerClickedOnNestBuildButton() 
        {
            EventBus.RaiseEvent<IPlayerChoosesNestCellHandler>(it => it.StartState(this));
            _buildView.OnClick -= PlayerClickedOnNestBuildButton;
            Object.Destroy(_buildView.gameObject);
        }

        private Vector3 CalculateCenter()
        {
            Vector3 center = new();
            foreach (TerrainCell cell in _regionCells)
            {
                center += cell.transform.position;
            }
            return center / _regionCells.Count;
        }

        private void CreateRegionBorders()
        {
            GameObject go = (GameObject)Object.Instantiate(Resources.Load("ViewElements/RegionBorder"));
            go.transform.SetParent(_regionView.transform);
            _regionBoundes = go.GetComponent<RegionBorder>();
            _regionBoundes.AssignVertices(_regionCells, _cellType);
        }

        public void StartState(Region region)
        {
            if(region == this)
                _regionBoundes.HiliteBorders(Color.yellow);
        }

        public void EndState(Region region)
        {
            if (region == this)
                _regionBoundes.EndHiliteBorders();
        }

        public void OnNestDestroyed(Region region, TerrainCell cell)
        {
            if(region== this) 
            {
                isNestInRegion = false;
            }
        }
    }
}

