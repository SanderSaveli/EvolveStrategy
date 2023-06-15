using BattleSystem;
using EventBusSystem;
using System.Collections.Generic;
using TileSystem;
using UnityEngine;

public abstract class GameAcktor : ICellChangeOwnerHandler
{
    [SerializeField] public AcktorList acktorName { get; protected set; }
    [SerializeField] public Unit unit { get; protected set; }

    protected List<TerrainCell> _myCells = new();

    protected TerrainTilemap _terrainTilemap;
    private bool _isAcktivated;

    public GameAcktor(AcktorList acktorName)
    {
        _isAcktivated = false;
        this.acktorName = acktorName;
        this.unit = new(this);
        _terrainTilemap = GameObject.FindObjectOfType<TerrainTilemap>();
        _myCells = _terrainTilemap.GetStartCells(acktorName);
        EventBus.Subscribe(this);
    }
    ~GameAcktor()
    {
        EventBus.Unsubscribe(this);
    }

    public abstract void OfferToBuildNest(Region region);

    public void ChangeOwner(GameAcktor newOwner, TerrainCell cell)
    {
        if (_myCells.Contains(cell) && newOwner != this)
        {
            _myCells.Remove(cell);
            if (_myCells.Count == 0 && _isAcktivated)
            {
                EventBus.RaiseEvent<IAcktorDiedHandler>(it => it.AcktorDie(this));
            }
        }
        else if (newOwner == this)
        {
            _myCells.Add(cell);
            _isAcktivated |= true;
        }
    }
}
