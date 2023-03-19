using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

namespace TileSystem
{
    public class Region: IPlayerChoosesNestCellHandler, INestDestroyed
    {
        private List<TerrainCell> _regionCells = new();

        public bool isNestInRegion;

        private bool isRegionControledOnePlayer;
        private GameObject _regionView;
        private RegionBorder _regionBoundes;
        private NestBuildView _buildView;

        private CellType _cellType;

        private Dictionary<PlayersList, SubRegionView> _views = new();

        public bool isFade = true;

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
                cell.OnCellFilled += FindSubRegionForCell;
            }
            CreateRegionBorders();
            EventBus.Subscribe(this);
        }

        public List<TerrainCell> GetRegionCells() 
        {
            return _regionCells;
        }
        public void AddCell(TerrainCell cell) 
        {
            if (!_regionCells.Contains(cell)) 
            { 
                _regionCells.Add(cell);
                isNestInRegion |= cell.isNestBuilt;  
                FindSubRegionForCell(cell);
            }
        }

        public void ShowCellsInfo() 
        {
            //foreach (KeyValuePair<PlayersList, SubRegionView> pair in _views)
            //{
            //    pair.Value.ShowCellsInfo();
            //}
            _regionBoundes.ShowBorders();
            //isFade = false;
        }

        public void HideCellsInfo() 
        {
            //foreach(KeyValuePair<PlayersList, SubRegionView> pair in _views) 
            //{
            //    //pair.Value.HideCellsInfo();
            //}
            _regionBoundes.HideBorders();
            //isFade = true;
        }


        private void FindSubRegionForCell(TerrainCell cell) 
        {
            if (!_views.ContainsKey(cell.owner.acktorName))
                _views.Add(cell.owner.acktorName, CreateNewSubViewElement());
            _views[cell.owner.acktorName].AddCell(cell);
            if (isFade) 
            {
                _views[cell.owner.acktorName].HideCellsInfo();
            }
        }

        private SubRegionView CreateNewSubViewElement()
        {
            GameObject newViewObject = Object.Instantiate(Resources.Load<GameObject>("ViewElements/SubRegionView"));
            newViewObject.transform.SetParent(_regionView.transform, false);
            SubRegionView view = newViewObject.GetComponent<SubRegionView>();
            view.OnCellChangeOwner += FindSubRegionForCell;
            view.OnCellChangeOwner += NotifyIfRegionControlStatusChanged;
            view.OnEmptyView += DeleteEmptySubViewElement;
            return view;
        }

        private void DeleteEmptySubViewElement(GameAcktor owner)
        {
            Object.Destroy(_views[owner.acktorName].gameObject);
            _views.Remove(owner.acktorName);
        }

        private void NotifyIfRegionControlStatusChanged(TerrainCell cell) 
        { 
            if(IsOnePlayerControlRegion()) 
            {
                isRegionControledOnePlayer = true;
                EventBus.RaiseEvent<IRegionOwnershipStatusChangedHandler>(it => it.RegionControledBySinglePlayer(this, cell.owner));
                if(!isNestInRegion) 
                {
                    cell.owner.OfferToBuildNest(this);
                }
            }
            else if (isRegionControledOnePlayer) 
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
            Object.Destroy(_buildView);
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

