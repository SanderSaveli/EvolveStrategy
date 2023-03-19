using EventBusSystem;
using System.Collections.Generic;
using UnityEngine;
using TileSystem;

namespace BattleSystem
{
    public class NestBuilder : Singletone<NestBuilder>, IRegionOwnershipStatusChangedHandler
    {
        private List<Region> _avalibleForNestBuilding = new();

        public NestBuilder()
        {
            EventBus.Subscribe(this);
        }

        public void RegionControledBySinglePlayer(Region region, GameAcktor owner)
        {
            if (!_avalibleForNestBuilding.Contains(region) && !region.isNestInRegion)
            {
                _avalibleForNestBuilding.Add(region);
            }
        }

        public void RegionNoLongerControlledBySinglePlayer(Region region)
        {
            if (_avalibleForNestBuilding.Contains(region))
            {
                _avalibleForNestBuilding.Remove(region);
            }
        }

        public bool TryBuildNest(TerrainCell cell)
        {
            if (_avalibleForNestBuilding.Contains(cell.region))
            {
                BuildNest(cell);
                cell.region.isNestInRegion = true;
                return true;
            }
            return false;
        }

        private void BuildNest(TerrainCell cell)
        {
            cell.isNestBuilt = true;
            _avalibleForNestBuilding.Remove(cell.region);
        }
    }
}

