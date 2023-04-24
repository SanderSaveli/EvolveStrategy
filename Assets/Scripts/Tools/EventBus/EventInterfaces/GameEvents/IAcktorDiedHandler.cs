namespace EventBusSystem
{
    public interface IAcktorDiedHandler : IGlobalSubscriber
    {
        public void AcktorDie(GameAcktor acktor);
    }
}