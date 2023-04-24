using TileSystem;

namespace EventBusSystem
{
    public interface INestDestroyed : IGlobalSubscriber
    {
        public void OnNestDestroyed(Region region, TerrainCell cell);
    }
}
