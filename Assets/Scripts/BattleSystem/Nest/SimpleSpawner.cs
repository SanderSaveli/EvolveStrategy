using BattleSystem;
using TileSystem;

public class SimpleSpawner : ICellBased
{
    private Timer _timer = new();
    protected TerrainCell _cell;
    private Unit _unit;
    protected float DEFAULT_TIME_TO_SPAWN = 1;
    public SimpleSpawner(TerrainCell cell)
    {
        _cell = cell;
        _unit = cell.owner.unit;
        StartTimerToSpawnUnit();
    }
    ~SimpleSpawner()
    {
        _timer.StopTimer();
    }

    private void StartTimerToSpawnUnit()
    {
        _timer.StartTimer(DEFAULT_TIME_TO_SPAWN * _unit.spawnSpeed);
        _timer.OnTimeOver -= SpawnUnit;
        _timer.OnTimeOver += SpawnUnit;
    }

    protected virtual void SpawnUnit()
    {
        _cell.unitNumber++;
        StartTimerToSpawnUnit();
    }
}
