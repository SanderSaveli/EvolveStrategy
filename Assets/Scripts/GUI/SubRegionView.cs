using System.Collections.Generic;
using System.Linq.Expressions;
using TileSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubRegionView : MonoBehaviour
{
    public delegate void CellChangeOwner(TerrainCell cell);
    public event CellChangeOwner OnCellChangeOwner;

    public delegate void EmptyView(PlayersList owner);
    public event EmptyView OnEmptyView;

    private TextMeshProUGUI _unitsNumberTxt =>
        GetComponentInChildren<TextMeshProUGUI>();

    private Image _nestIcon =>
        GetComponentInChildren<Image>();

    private List<TerrainCell> _cells = new();

    [SerializeField]private int _unitNumber;
    private int _foodNumber;
    private bool _isNestBuilt;
    private PlayersList _owner;

    private bool _isShowen = true;
    private int unitNumber
    {
        get => _unitNumber;
        set { _unitNumber = value; 
            UpdateUnitView(); }
    }

    private int foodNumber
    {
        get => _foodNumber;
        set { _foodNumber = value; 
            UpdateFoodView(); }
    }

    private bool isNestBuilt
    {
        get => _isNestBuilt;
        set { _isNestBuilt = value; 
            UpdateNestView(); }
    }

    private PlayersList owner
    {
        get => _owner;
        set { _owner = value;
            UpdateOwnerView(); }
    }


    private void Awake()
    {
        transform.transform.localScale =
            gameObject.GetComponentInParent<Transform>().localScale;
    }
    private void OnEnable()
    {
        foreach (TerrainCell cell in _cells)
        {
            SubscribeToAllChangeIvennts(cell);
        }
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
        if (_cells.Count != 0)
        {
            foreach (TerrainCell cell in _cells)
            {
                cell.ShowView();
            }
            HideGeneralInfo();
        }
    }

    public void HideCellsInfo()
    {
        if (_cells.Count != 0)
        {
            foreach (TerrainCell cell in _cells)
            {
                cell.HideView();
            }
            ShowGeneralInfo();
        }
    }

    public void AddCell(TerrainCell cell)
    {
        if (!_cells.Contains(cell))
        {
            SubscribeToAllChangeIvennts(cell);
            _cells.Add(cell);
            if (_cells.Count == 0)
            {
                owner = cell.owner;
            }
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
        Color col = _unitsNumberTxt.faceColor;
        col.a = 0;
        _unitsNumberTxt.faceColor = col;
        col = _nestIcon.color;
        col.a = 0;
        _nestIcon.color = col;
    }
    private void ShowGeneralInfo() 
    {
        _isShowen = true;
        Color col = _unitsNumberTxt.faceColor;
        col.a = 1;
        _unitsNumberTxt.faceColor = col;
        col = _nestIcon.color;
        col.a = isNestBuilt? 1 : 0;
        _nestIcon.color = col;
    }

    private void UpdateUnitView()
    {
        _unitsNumberTxt.text = unitNumber.ToString();
        Color ownerColor = new PlayersColors().GetColor(_cells[0].owner);
        ownerColor.a = _isShowen ? 1 :0 ;
        _unitsNumberTxt.faceColor = ownerColor;
    }

    private void UpdateNestView()
    {
        if (_isShowen) 
        { 
            Color col = _nestIcon.color;
            col.a = isNestBuilt ? 1 : 0;
            _nestIcon.color = col;
        }
    }

    private void UpdateFoodView() 
    {

    }

    private void UpdateOwnerView()
    {
        Color col = _nestIcon.color;
        col.a = isNestBuilt ? 1 : 0;
        _nestIcon.color = col;
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
    private void ChangeOwner(PlayersList previousOwner, PlayersList newOwner, TerrainCell cell)
    {
        if(newOwner!= owner) 
        {
            OnCellChangeOwner?.Invoke(cell);
            RemoveCell(cell);
            if(_cells.Count == 0) 
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
