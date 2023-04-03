using System.Collections;
using TileSystem;
using UnityEngine;

namespace BattleSystem
{
    public class CellSpawner : ICellSpawner
    {
        protected TerrainCell _cell;
        private Unit _unit;

        private Coroutine _spawnCorutine;
        private Coroutine _killCorutine;

        ISpawnCondition _condition;

        public CellSpawner(TerrainCell cell, ISpawnCondition startCondition)
        {
            _cell = cell;
            _unit = cell.owner.unit;
            ChangeCondition(startCondition);
            if (cell.cellType.climate != ClimateType.Temperate)
            {
                _killCorutine = Coroutines.StartRoutine(StartKillingUnits());
            }
        }

        ~CellSpawner()
        {
            Coroutines.StopRoutine(_spawnCorutine);
            Coroutines.StopRoutine(_killCorutine);
        }
        public void ChangeCondition(ISpawnCondition condition)
        {
            if(_spawnCorutine != null)
                Coroutines.StopRoutine(_spawnCorutine);
            _condition = condition;
            _spawnCorutine = Coroutines.StartRoutine(_condition.StartSpawningUnits(_cell));
        }

        IEnumerator StartKillingUnits()
        {
            while (true)
            {
                yield return new WaitForSeconds(CalculateUnitDeathTime(_cell.cellType.climate));
                killUnit();
            }
        }

        protected virtual void killUnit()
        {
            _cell.unitNumber--;
        }

        private float CalculateUnitDeathTime(ClimateType climate)
        {
            switch (climate)
            {
                case (ClimateType.Hot):
                    return BattleConstants.DEFAULT_TIME_TO_DIE * _unit.heatResistance + BattleConstants.DEFAULT_TIME_TO_DIE;
                case (ClimateType.Cold):
                    return BattleConstants.DEFAULT_TIME_TO_DIE * _unit.coldResistance + BattleConstants.DEFAULT_TIME_TO_DIE;
                case (ClimateType.Poison):
                    return BattleConstants.DEFAULT_TIME_TO_DIE * _unit.poisonResistance + BattleConstants.DEFAULT_TIME_TO_DIE;
            }
            return BattleConstants.DEFAULT_TIME_TO_DIE;
        }
    }
}

