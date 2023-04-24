using TileSystem;
using static BattleSystem.AttackCell;

namespace BattleSystem 
{
    public interface IAttackComand : IComand
    {
        public GameAcktor GetAttackingPlayer();

        public delegate void AttackEnd(TerrainCell to, Unit unit, int unitCount);
        public event AttackEnd OnAttackEnd;
    }
}