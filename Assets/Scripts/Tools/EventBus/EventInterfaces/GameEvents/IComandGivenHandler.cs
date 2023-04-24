using BattleSystem;
using TileSystem;
namespace EventBusSystem
{
    public interface IComandGivenHandler : IGlobalSubscriber
    {
        public void OnGivenComandToAttack(TerrainCell from, TerrainCell to, IAttackComand comand);
    }
}
