using EventBusSystem;
using System.Collections.Generic;
using TileSystem;
using UnityEngine;

namespace BattleSystem
{
    public class BattleService : IService
    {
        private TerrainTilemap _terrainTilemap;

        private List<IComand> _comandList;

        public void StartWork()
        {
            _comandList = new();
            _terrainTilemap = Object.FindObjectOfType<TerrainTilemap>();
        }

        public void EndWork()
        {
            _comandList.Clear();
        }

        public void TryGiveOrderToAttackHalfUnit(Vector3 from, Vector3 to, GameAcktor acktor)
        {
            if (IsConditionsCorrect(from, to, acktor))
            {
                TerrainCell fromCell = _terrainTilemap.GetTile(from);
                TerrainCell toCell = _terrainTilemap.GetTile(to);
                TryGiveOrderToAttackHalfUnit(fromCell, toCell, acktor);
            }
        }
        public void TryGiveOrderToAttackHalfUnit(TerrainCell from, TerrainCell to, GameAcktor acktor)
        {
            if (IsConditionsCorrect(from, to, acktor))
                GiveOrderToAttack(from, to, from.unitNumber / 2);
        }

        public void TryGiveOrderToAttackAllUnit(Vector3 from, Vector3 to, GameAcktor acktor)
        {
            if (IsConditionsCorrect(from, to, acktor))
            {
                TerrainCell fromCell = _terrainTilemap.GetTile(from);
                TerrainCell toCell = _terrainTilemap.GetTile(to);
                TryGiveOrderToAttackAllUnit(fromCell, toCell, acktor);
            }
        }

        public void TryGiveOrderToAttackAllUnit(TerrainCell from, TerrainCell to, GameAcktor acktor)
        {
            if (IsConditionsCorrect(from, to, acktor))
                GiveOrderToAttack(from, to, from.unitNumber);
        }
        private void GiveOrderToAttack(TerrainCell from, TerrainCell to, int unitsSent)
        {
            Unit attackingUnit = from.owner.unit;
            AttackCell attackComand = new(to, attackingUnit, unitsSent,
                attackingUnit.GetMoveDuration(to.cellType.move));
            from.unitNumber -= unitsSent;
            attackComand.OnAttackEnd += ChoseBattleSituation;
            attackComand.OnComandEnd += RemoveComand;
            _comandList.Add(attackComand);
            EventBus.RaiseEvent<IComandGivenHandler>(it => it.OnGivenComandToAttack(from, to, attackComand));
        }

        private bool IsConditionsCorrect(TerrainCell from, TerrainCell to, GameAcktor acktor)
        {
            if (_terrainTilemap.isCellsNeighbours(to, from) && from.unitNumber >= 1 && from.owner == acktor)
            {
                return true;
            }
            return false;
        }
        private bool IsConditionsCorrect(Vector3 cellA, Vector3 cellB, GameAcktor acktor)
        {
            if (_terrainTilemap.ContainTile(cellA)
                && _terrainTilemap.ContainTile(cellB))
            {
                TerrainCell from = _terrainTilemap.GetTile(cellA);
                TerrainCell to = _terrainTilemap.GetTile(cellB);
                return IsConditionsCorrect(from, to, acktor);
            }
            return false;
        }

        private void ChoseBattleSituation(TerrainCell to, Unit unit, int unitCount)
        {
            if (unit.owner != to.owner)
            {
                IntatiateBattle(to, unitCount, unit);
            }
            else
            {
                to.unitNumber += unitCount;
            }

        }

        private void IntatiateBattle(TerrainCell cell, int attackUnitCount, Unit attackUnit)
        {
            Unit defanceUnit = cell.owner.unit;

            int defanceUnitCount = cell.unitNumber;
            while (defanceUnitCount > 0 && attackUnitCount > 0)
            {
                int mutalDefenderAttack = defanceUnit.attack * defanceUnitCount;
                int mutalAttackingAttack = attackUnit.attack * attackUnitCount;

                int killedDefenderUnits = (int)Mathf.Floor(mutalAttackingAttack / defanceUnit.defense);
                int killedAttackingUnits = (int)Mathf.Floor(mutalDefenderAttack / attackUnit.defense);

                attackUnitCount -= killedAttackingUnits;
                defanceUnitCount -= killedDefenderUnits;
            }

            if (attackUnitCount > 0)
            {
                cell.owner = attackUnit.owner;
                cell.unitNumber = attackUnitCount;
            }
            else
            {
                cell.unitNumber = defanceUnitCount > 0 ? defanceUnitCount : 0;
            }
        }

        private void RemoveComand(IComand comand)
        {
            _comandList.Remove(comand);
        }
    }
}

