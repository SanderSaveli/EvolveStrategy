using TileSystem;
using UnityEngine;

public class Nest : SimpleSpawner
{
    private Bank banck => Bank.instance;
    private const int EVOLVE_POINTS_PER_SPAWN = 1;

    public Nest(TerrainCell cell) : base(cell)
    {
        DEFAULT_TIME_TO_SPAWN = 1.5f;
    }

    protected override void SpawnUnit()
    {
        banck.AddPoints(_cell.owner.acktorName, EVOLVE_POINTS_PER_SPAWN);
        base.SpawnUnit();
    }
}
