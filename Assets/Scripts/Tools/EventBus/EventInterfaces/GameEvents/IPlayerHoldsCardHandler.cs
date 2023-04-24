namespace EventBusSystem
{
    public interface IPlayerHoldsCardHandler : IGlobalSubscriber
    {
        public void PlayerStartHoldCard();
        public void PlayerStopHoldCard();
    }
}
