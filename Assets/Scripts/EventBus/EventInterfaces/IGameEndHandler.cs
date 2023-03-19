namespace EventBusSystem
{
    public interface IGameEndHandler : IGlobalSubscriber
    {
        public void PlayerWin();
        public void PlayerLose();
    }
}