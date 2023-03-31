using BattleSystem;
using System.Collections;
using TileSystem;
using UnityEngine;

public class SimpleSpawner : ICellBased
{
    protected TerrainCell _cell;
    private Unit _unit;

    private Coroutine spawnCorutine;
    private Coroutine killCorutine;
    protected float DEFAULT_TIME_TO_SPAWN = 4;
    protected float DEFAULT_TIME_TO_DIE = 3;
    public bool isAcktive { get; set; }
    public SimpleSpawner(TerrainCell cell)
    {
        isAcktive = true;
        _cell = cell;
        _unit = cell.owner.unit;
        StartTimerToSpawnUnit();
        if(cell.cellType.climate != ClimateType.Temperate) 
        {
            StartTimerToKillUnit();
        }
    }

    private void StartTimerToSpawnUnit()
    {
        spawnCorutine = Coroutines.StartRoutine(SpawnUnits());
    }
    private void StartTimerToKillUnit()
    {
        killCorutine = Coroutines.StartRoutine(KillUnits());
    }

    protected virtual void SpawnUnit()
    {
        _cell.unitNumber++;
    }
    protected virtual void killUnit()
    {
        _cell.unitNumber--;
    }

    IEnumerator SpawnUnits() 
    {
        while (isAcktive) 
        {
            yield return new WaitForSeconds(DEFAULT_TIME_TO_SPAWN / _unit.spawnSpeed);
            SpawnUnit();
        }
        Coroutines.StopRoutine(spawnCorutine);
    }

    IEnumerator KillUnits()
    {
        while (isAcktive)
        {
            yield return new WaitForSeconds(CalculateUnitDeathTime(_cell.cellType.climate));
            killUnit();
        }
        Coroutines.StopRoutine(killCorutine);
    }
    private float CalculateUnitDeathTime(ClimateType climate) 
    {
        switch (climate) 
        {
            case (ClimateType.Temperate):
                return 0;
            case (ClimateType.Hot):
                return DEFAULT_TIME_TO_DIE + _unit.heatResistance * 3;
            case (ClimateType.Cold):
                return DEFAULT_TIME_TO_DIE + _unit.coldResistance * 3;
            case (ClimateType.Poison):
                return DEFAULT_TIME_TO_DIE + _unit.poisonResistance * 3;
        }
        return DEFAULT_TIME_TO_DIE;
    } 
}
