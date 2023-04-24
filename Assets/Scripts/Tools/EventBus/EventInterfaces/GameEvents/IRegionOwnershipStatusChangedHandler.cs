using TileSystem;

namespace EventBusSystem
{
    public interface IRegionOwnershipStatusChangedHandler : IGlobalSubscriber
    {
        public void RegionControledBySinglePlayer(Region region, GameAcktor owner);
        public void RegionNoLongerControlledBySinglePlayer(Region region);
    }
}
