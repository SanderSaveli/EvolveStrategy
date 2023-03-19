using BattleSystem;
using TileSystem;
using UnityEngine;
using System.Collections.Generic;
using EventBusSystem;

public abstract class GameAcktor : ICellChangeOwnerHandler
{
    [SerializeField] public PlayersList acktorName { get; protected set; }
    [SerializeField] public Unit unit { get; protected set; }

    protected List<TerrainCell> _myCells = new();

    protected TerrainTilemap _terrainTilemap;
    private bool _isAcktivated;
    
    public GameAcktor(PlayersList acktorName, TerrainTilemap terrainTilemap)
    {
        _isAcktivated = false;
        this.acktorName = acktorName;
        this.unit = new(this);
        _terrainTilemap = terrainTilemap;
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
