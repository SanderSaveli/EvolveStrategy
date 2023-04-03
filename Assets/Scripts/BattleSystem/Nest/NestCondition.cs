using System.Collections;
using TileSystem;
using UnityEngine;

namespace BattleSystem
{
    public class NestCondition : ISpawnCondition
    {
        private Bank _banck;
        private TerrainCell _cell;  
        private Unit _unit;
        public NestCondition()
        {
            _banck = Bank.instance;
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
            return _unit.spawnSpeed * BattleConstants.DEFAULT_TIME_TO_NEST_SPAWN;
        }

        private void SpawnUnit()
        {
            _banck.AddPoints(_cell.owner.acktorName, BattleConstants.EVOLVE_POINTS_PER_SPAWN);
            _cell.unitNumber++;
        }
    }
}

