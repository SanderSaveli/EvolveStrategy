using TileSystem;

namespace EventBusSystem
{
    public interface ICellChangeUnitNumberHandler : IGlobalSubscriber
    {
        public void CellChangeOwner(int previousUnitNumber, int newUnitNumber, TerrainCell cell);
    }
}
