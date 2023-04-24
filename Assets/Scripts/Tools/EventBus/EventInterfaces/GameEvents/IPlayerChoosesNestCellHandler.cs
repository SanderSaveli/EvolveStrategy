using TileSystem;

namespace EventBusSystem
{
    public interface IPlayerChoosesNestCellHandler : IGlobalSubscriber
    {
        public void StartState(Region region);
        public void EndState(Region region);
    }
}

