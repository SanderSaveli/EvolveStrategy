using TileSystem;
using UnityEngine;
using System.Collections;

namespace BattleSystem
{
    public interface ISpawnCondition
    {
        IEnumerator StartSpawningUnits(TerrainCell cell);
    }
}
