using System.Collections.Generic;
using TileSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubRegionView : MonoBehaviour
{
    public delegate void CellChangeOwner(TerrainCell cell);
    public event CellChangeOwner OnCellChangeOwner;

    public delegate void EmptyView(GameAcktor owner);
    public event EmptyView OnEmptyView;

    private TextMeshProUGUI _unitsNumberTxt =>
        GetComponentInChildren<TextMeshProUGUI>();

    private Image _nestIcon =>
        GetComponentInChildren<Image>();

    private List<TerrainCell> _cells = new();

    private ViewFaider faider;

    [SerializeField] private float _fadeInDuration = 0.5f;
    [SerializeField] private float _fadeOutDuration = 0.5f;
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
            UpdateNestView();
        }
    }

    private GameAcktor owner
    {
        get => _owner;
        set
        {
            _owner = value;
            UpdateOwnerView();
        }
    }


    private void Awake()
    {
        faider = new();
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
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Image image))
            {
                faider.FadeOut(image, _fadeOutDuration);
            }
            if (transform.GetChild(i).TryGetComponent(out TextMeshProUGUI text))
            {
                faider.FadeOut(text, _fadeOutDuration);
            }
        }
    }
    private void ShowGeneralInfo()
    {
        _isShowen = true;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Image image))
            {
                faider.FadeIn(image, _fadeOutDuration);
            }
            if (transform.GetChild(i).TryGetComponent(out TextMeshProUGUI text))
            {
                faider.FadeIn(text, _fadeOutDuration);
            }
        }
    }

    private void UpdateUnitView()
    {
        _unitsNumberTxt.text = unitNumber.ToString();
        Color ownerColor = new PlayersColors().GetColor(_cells[0].owner.acktorName);
        ownerColor.a = _isShowen ? 1 : 0;
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
    private void ChangeOwner(GameAcktor previousOwner, GameAcktor newOwner, TerrainCell cell)
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
