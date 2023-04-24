using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TileSystem;
using BattleSystem;

public class BattleBot : GameAcktor
{
    private Bank _bank;
    private BattleService _battleManager;
    private NestBuilder _builder;

    [SerializeField] private AcktorList _me;
    [SerializeField] private float TimeToOneTurn;
    private Coroutine currentCorutine;
    private int level;

    public BattleBot(AcktorList acktorName): 
        base(acktorName)
    {
        _battleManager = ServiceLocator.Get<BattleService>();
        _builder = NestBuilder.instance;
        _bank = Bank.instance;
        level = 1;
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
            TerrainCell[] cellsInthisTurn = _myCells.ToArray();
            foreach (TerrainCell cell in cellsInthisTurn) 
            {
                float delay = Random.Range(0.5f, 1);
                yield return new WaitForSeconds(delay);
                MakeDesigion(cell);
            }
        }
    }
    public void UpgradeUnit() 
    {
        if (_bank.TryToBuy(acktorName, 110)) 
        {
            if(level < 4) 
            {
                level++;
                unit.attack += 5;
                unit.defense += 5;
                unit.poisonResistance += 0.125f;
                unit.coldResistance += 0.125f;
                unit.heatResistance += 0.125f;
                unit.spawnSpeed += 0.125f;
                unit.climbSpeed += 0.125f;
                unit.walckSpeed += 0.125f;
                unit.swimSpeed += 0.125f;
            }
        }
    }
    private void MakeDesigion(TerrainCell cell) 
    { 
        if(cell.owner == this) 
        {
            List<TerrainCell> cellToAnalysis = _terrainTilemap.GetCellNeighbors(cell);
            TerrainCell friendMaxCell = null;
            TerrainCell enemyMinCell = null;
            TerrainCell enemyMaxCell = null;
            foreach (TerrainCell currentCell in cellToAnalysis)
            {
                if (currentCell.owner == this)
                {
                    if (friendMaxCell == null)
                    {
                        friendMaxCell = currentCell;
                    }
                    else
                    {
                        if (friendMaxCell.unitNumber < currentCell.unitNumber)
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
                        if (enemyMinCell.unitNumber > currentCell.unitNumber)
                        {
                            enemyMinCell = currentCell;
                        }
                    }
                }
            }
            if (enemyMinCell != null)
            {
                if (enemyMinCell.unitNumber < cell.unitNumber)
                {
                    _battleManager.TryGiveOrderToAttackAllUnit(cell, enemyMinCell, this);
                    return;
                }
            }
            if (enemyMaxCell != null)
            {
                if (enemyMaxCell.unitNumber > cell.unitNumber)
                {
                    return;
                }
            }
            if (friendMaxCell != null)
            {
                if (cell.unitNumber > 10 && cell.unitNumber < friendMaxCell.unitNumber)
                {
                    _battleManager.TryGiveOrderToAttackHalfUnit(cell, friendMaxCell, this);
                }
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
