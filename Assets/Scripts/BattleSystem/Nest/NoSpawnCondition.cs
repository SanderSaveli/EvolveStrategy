using System.Collections;
using TileSystem;

namespace BattleSystem
{
    public class NoSpawnCondition : ISpawnCondition
    {
        public IEnumerator StartSpawningUnits(TerrainCell cell)
        {   
            yield break;
        }
    }
}
