using System.Collections.Generic;
using TileSystem;
using UnityEngine;

public class SubRegionView : MonoBehaviour
{
    public delegate void CellChangeOwner(TerrainCell cell);
    public event CellChangeOwner OnCellChangeOwner;

    public delegate void EmptyView(GameAcktor owner);
    public event EmptyView OnEmptyView;

    private UnitText _unitsNumberTxt;

    private NestView _nestIcon;

    private List<TerrainCell> _cells = new();

    private ViewFaider _faider;

    [SerializeField] private int _unitNumber;
    private int _foodNumber;
    private bool _isNestBuilt;
    private GameAcktor _owner;

    private bool _isShowen = true;
    private int unitNumber
    {
        get => _unitNumber;
        set
        {
            _unitNumber = value;
            UpdateUnitView();
        }
    }

    private int foodNumber
    {
        get => _foodNumber;
        set
        {
            _foodNumber = value;
            UpdateFoodView();
        }
    }

    private bool isNestBuilt
    {
        get => _isNestBuilt;
        set
        {
            _isNestBuilt = value;
            UpdateNestView(value);
        }
    }

    private GameAcktor owner
    {
        get => _owner;
        set
        {
            _owner = value;
            UpdateUnitView();
        }
    }


    private void Awake()
    {
        _faider = new();
        transform.transform.localScale =
            gameObject.GetComponentInParent<Transform>().localScale;
        _nestIcon = GetComponentInChildren<NestView>();
        _unitsNumberTxt = GetComponentInChildren<UnitText>();
    }
    private void OnEnable()
    {
        foreach (TerrainCell cell in _cells)
        {
            SubscribeToAllChangeIvennts(cell);
        }
        HideGeneralInfo();
    }
    private void OnDisable()
    {
        foreach (TerrainCell cell in _cells)
        {
            UnSubscribeToAllChangeIvennts(cell);
        }
    }
    private void OnDestroy()
    {
        foreach (TerrainCell cell in _cells)
        {
            UnSubscribeToAllChangeIvennts(cell);
        }
        _cells.Clear();
    }

    public void ShowCellsInfo()
    {
        //if (_cells.Count != 0)
        //{
        //    foreach (TerrainCell cell in _cells)
        //    {
        //        cell.ShowView();
        //    }
        //    HideGeneralInfo();
        //}
    }

    public void HideCellsInfo()
    {
        //if (_cells.Count != 0)
        //{
        //    foreach (TerrainCell cell in _cells)
        //    {
        //        cell.HideView();
        //    }
        //    ShowGeneralInfo();
        //}
    }

    public void AddCell(TerrainCell cell)
    {
        if (!_cells.Contains(cell))
        {
            SubscribeToAllChangeIvennts(cell);
            if (_cells.Count == 0)
            {
                owner = cell.owner;
            }
            _cells.Add(cell);
            unitNumber += cell.unitNumber;
            foodNumber += cell.foodNumber;
            isNestBuilt |= cell.isNestBuilt;
            transform.position = CalculateCenter();
        }
    }

    public void RemoveCell(TerrainCell cell)
    {
        UnSubscribeToAllChangeIvennts(cell);
        unitNumber -= cell.unitNumber;
        foodNumber -= cell.foodNumber;
        isNestBuilt &= !cell.isNestBuilt;
        _cells.Remove(cell);
    }

    private void HideGeneralInfo()
    {
        _isShowen = false;
        _faider.FadeOutAllView(transform);
    }
    private void ShowGeneralInfo()
    {
        _isShowen = true;
        //_faider.FadeInAllView(transform);
    }

    private void UpdateUnitView()
    {
        _unitsNumberTxt.ChangeText(unitNumber.ToString());
        Color col = new PlayersColors().GetColor(owner.acktorName);
        _unitsNumberTxt.ChangeColorTo(col);
    }

    private void UpdateNestView(bool isBuilt)
    {
        _nestIcon.gameObject.SetActive(isBuilt);
        if (isBuilt)
        {
            _nestIcon.ChangeTransparency(_isShowen ? 1 : 0);
        }
    }

    private void UpdateFoodView()
    {

    }

    private Vector3 CalculateCenter()
    {
        Vector3 center = new();
        foreach (TerrainCell cell in _cells)
        {
            center += cell.transform.position;
        }
        return center / _cells.Count;
    }

    private void SubscribeToAllChangeIvennts(TerrainCell cell)
    {
        cell.OnOwnerChenge += ChangeOwner;
        cell.OnUnitNumberChenge += ChangeUnitNumber;
        cell.OnNestConditionChenge += ChangeNestCondition;
        cell.OnFoodNumberChenge += ChangeFoodNumber;
    }

    private void UnSubscribeToAllChangeIvennts(TerrainCell cell)
    {
        cell.OnOwnerChenge -= ChangeOwner;
        cell.OnUnitNumberChenge -= ChangeUnitNumber;
        cell.OnNestConditionChenge -= ChangeNestCondition;
        cell.OnFoodNumberChenge -= ChangeFoodNumber;
    }
    private void ChangeOwner(GameAcktor newOwner, TerrainCell cell)
    {
        if (newOwner != owner)
        {
            OnCellChangeOwner?.Invoke(cell);
            RemoveCell(cell);
            if (_cells.Count == 0)
            {
                OnEmptyView?.Invoke(_owner);
            }
        }
    }
    private void ChangeUnitNumber(int previousNumber, int newNumber, TerrainCell cell)
    {
        unitNumber += newNumber - previousNumber;
    }

    private void ChangeNestCondition(bool previousCondition, bool newCondition, TerrainCell cell)
    {
        isNestBuilt = newCondition;
    }

    private void ChangeFoodNumber(int previousNumber, int newNumber, TerrainCell cell)
    {
        foodNumber += newNumber - previousNumber;
    }
}
