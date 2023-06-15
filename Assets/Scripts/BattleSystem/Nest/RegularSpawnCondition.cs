using System.Collections;
using TileSystem;
using UnityEngine;

namespace BattleSystem
{
    public class RegularSpawnCondition : ISpawnCondition
    {
        private TerrainCell _cell;
        private Unit _unit;
        public void SpawnUnit()
        {
            _cell.unitNumber++;
        }

        public IEnumerator StartSpawningUnits(TerrainCell cell)
        {
            _cell = cell;
            _unit = cell.owner.unit;
            while (true)
            {
                yield return new WaitForSeconds(CalculateUnitSpawnTime());
                SpawnUnit();
            }
        }

        private float CalculateUnitSpawnTime()
        {
            return _unit.spawnSpeed * BattleConstants.DEFAULT_TIME_TO_SPAWN;
        }
    }
}

