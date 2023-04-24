namespace EventBusSystem
{
    public interface IEvolvePointsChangeHandler : IGlobalSubscriber
    {
        public void EvolvePointsChanges(AcktorList acktor, int value);
    }
}


