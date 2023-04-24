namespace EventBusSystem 
{ 
    public interface ISetStartOwnerHandler : IGlobalSubscriber
    {
        public void SetStartOwner(GameAcktor owner);
    }
}


