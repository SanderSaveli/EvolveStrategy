using TileSystem;

namespace EventBusSystem
{
    public interface IPlayerChoosesNestCellHandler : IGlobalSubscriber
    {
        public void StartChoiseState(Region region);
        public void EndChoiseState(Region region);
    }
}

