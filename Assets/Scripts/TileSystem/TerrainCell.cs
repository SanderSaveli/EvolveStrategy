using EventBusSystem;
using UnityEngine;
using BattleSystem;

namespace TileSystem
{
    public class TerrainCell : MonoBehaviour
    {
        #region Events
        public delegate void OwnerChenge(GameAcktor newOwner, TerrainCell cell);
        public event OwnerChenge OnOwnerChenge;

        public delegate void UnitNumberChenge(int previousNumber, int newNumber, TerrainCell cell);
        public event UnitNumberChenge OnUnitNumberChenge;

        public delegate void NestConditionChenge(bool preciousState, bool newState, TerrainCell cell);
        public event NestConditionChenge OnNestConditionChenge;

        public delegate void FoodNumberChenge(int previousNumber, int newNumber, TerrainCell cell);
        public event UnitNumberChenge OnFoodNumberChenge;
        #endregion

        private CellView _view;
        public CellType cellType;

        private Region _region = null;

        private bool _isShowen = true;
        private ICellSpawner building;

        [SerializeField] public AcktorList startOwner;
        private GameAcktor _owner;
        [SerializeField] private int _unitNumber;
        [SerializeField] private int _foodNumber;
        [SerializeField] private bool _isNestBuilt;

        public Region region 
        { 
            get => _region;
            set { _region = value; } 
        }
        public GameAcktor owner
        {
            get 
            {
                if(_owner == null) 
                {
                    _owner = ServiceLocator.Get<GameHost>().GetAcktorByEnum(startOwner);
                }
                return _owner;
            }
            set 
            {
                if(_owner != value) 
                {
                    _owner = value;
                    DestroyNest();
                    building.ChangeCondition(new RegularSpawnCondition());
                    if (value != null)
                        EventBus.RaiseEvent<ICellChangeOwnerHandler>(it => it.ChangeOwner(value, this));
                    OnOwnerChenge?.Invoke(owner, this);
                }
            }
        }
        public int unitNumber
        {
            get => _unitNumber;
            set 
            { 
                if(value >= 0) 
                {
                    OnUnitNumberChenge?.Invoke(_unitNumber, value, this);
                    _unitNumber = value;
                    UpdateUnitView();
                }
            } 
        }
        
        public int foodNumber
        {
            get => _foodNumber;
            set
            {
                OnFoodNumberChenge?.Invoke(_foodNumber, value, this);
                _foodNumber = value;
                UpdateFoodView();
            }
        }
        public bool isNestBuilt
        {
            get => _isNestBuilt;
            set
            {
                OnNestConditionChenge?.Invoke(_isNestBuilt, value, this);
                _isNestBuilt = value;
                UpdateNestView();
            }
        }
        private void Start()
        {
            _view = GetComponentInChildren<CellView>();
            _owner = ServiceLocator.Get<GameHost>().GetAcktorByEnum(startOwner);
            if (startOwner!= AcktorList.None) 
            {
                if (_isNestBuilt) 
                {
                    building = new CellSpawner(this, new NestCondition());
                }
                else 
                {
                    building = new CellSpawner(this, new RegularSpawnCondition());
                }
            }
            else 
            {
                building = new CellSpawner(this, new NoSpawnCondition());
            }
            UpdateNestView();
            UpdateUnitView();
        }

        private SpriteRenderer _spriteRenderer => gameObject.GetComponent<SpriteRenderer>();

        public void ChangeColorTo(Color color) 
        { 
            _spriteRenderer.color = color;
        }

        public void HideView() 
        {
            _isShowen = false;
            _view.HideView();
            ChangeColorTo(Color.gray);
        }

        public void ShowView() 
        {           
            _isShowen = true;
            _view.ShowView(_isNestBuilt);
            ChangeColorTo(Color.white);
        }
        private void UpdateUnitView() 
        {
            _view.UpdateUnitView(_unitNumber, _owner.acktorName, _isShowen);
        }

        private void UpdateNestView() 
        {
            _view.UpdateNestView(_isNestBuilt, _isShowen);
        }

        private void UpdateFoodView() 
        { 
            
        }

        private void DestroyNest() 
        {
            if (isNestBuilt) 
            {
                building.ChangeCondition(new RegularSpawnCondition());
                EventBus.RaiseEvent<INestDestroyed>(it => it.OnNestDestroyed(region, this));
                isNestBuilt = false;
            }
        }
    }
}

