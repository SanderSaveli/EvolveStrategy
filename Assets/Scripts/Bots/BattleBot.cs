using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TileSystem;
using BattleSystem;

public class BattleBot : GameAcktor
{
    private BattleManager _battleManager;
    private NestBuilder _builder;

    [SerializeField] private PlayersList _me;
    [SerializeField] private float TimeToOneTurn;
    private Coroutine currentCorutine;

    public BattleBot(PlayersList acktorName, TerrainTilemap terrainTilemap): 
        base(acktorName, terrainTilemap)
    {
        _battleManager = BattleManager.instance;
        _builder = NestBuilder.instance;
    }

    public void StartBot() 
    {
        currentCorutine = Coroutines.StartRoutine(AnalysisSituationAndMakeTurn());
    }

    public void StopBot() 
    {
        Coroutines.StopRoutine(currentCorutine);
    }

    IEnumerator AnalysisSituationAndMakeTurn() 
    {
        while (_myCells.Count > 0) 
        {
            yield return new WaitForSeconds(TimeToOneTurn);
            foreach (TerrainCell cell in _myCells) 
            {
                MakeDesigion(cell);
            }
        }
    }

    private void MakeDesigion(TerrainCell cell) 
    { 
        List<TerrainCell> cellToAnalysis = _terrainTilemap.GetCellNeighbors(cell);
        TerrainCell friendMaxCell = null;
        TerrainCell enemyMinCell = null;
        TerrainCell enemyMaxCell = null;
        foreach (TerrainCell currentCell in cellToAnalysis) 
        { 
            if(currentCell.owner == this) 
            { 
                if(friendMaxCell == null)
                { 
                    friendMaxCell = currentCell;
                }
                else 
                { 
                    if(friendMaxCell.unitNumber < currentCell.unitNumber) 
                    {
                        friendMaxCell = currentCell;
                    }
                }
            }
            else 
            {
                if (enemyMinCell == null)
                {
                    enemyMinCell = currentCell;
                }
                else 
                { 
                    if(enemyMinCell.unitNumber > currentCell.unitNumber) 
                    {
                        enemyMinCell = currentCell;
                    }
                }
            }
        }
        if(enemyMinCell != null) 
        {
            if(enemyMinCell.unitNumber < cell.unitNumber) 
            {
                _battleManager.TryGiveOrderToAttackAllUnit(cell, enemyMinCell, this);
                return;
            }
        }
        if(enemyMaxCell != null) 
        { 
            if(enemyMaxCell.unitNumber > cell.unitNumber) 
            { 
                return;
            }
        }
        if(friendMaxCell != null) 
        { 
            if(cell.unitNumber > 10 && cell.unitNumber < friendMaxCell.unitNumber) 
            {
                _battleManager.TryGiveOrderToAttackHalfUnit(cell, friendMaxCell, this);
            }
        }
    }


    public override void OfferToBuildNest(Region region)
    {
        List<TerrainCell> cells = region.GetRegionCells();
        TerrainCell nestCell = cells[Random.Range(0, cells.Count-1)];
        _builder.TryBuildNest(nestCell);
    }
}
