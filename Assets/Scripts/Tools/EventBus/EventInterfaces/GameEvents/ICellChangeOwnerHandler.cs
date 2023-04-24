using TileSystem;

namespace EventBusSystem
{
    public interface ICellChangeOwnerHandler : IGlobalSubscriber
    {
        public void ChangeOwner(GameAcktor newOwner, TerrainCell cell);
    }
}

