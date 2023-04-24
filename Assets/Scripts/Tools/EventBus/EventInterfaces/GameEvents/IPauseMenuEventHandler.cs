namespace EventBusSystem 
{
    public interface IPauseMenuEventHandler : IGlobalSubscriber
    {
        public void OpenPause();
        public void ClousePause();

        public void BackToMenu();
        public void Restart();
    }

}