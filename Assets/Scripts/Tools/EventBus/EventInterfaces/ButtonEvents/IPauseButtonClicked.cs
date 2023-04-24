namespace EventBusSystem
{
    public interface IPauseButtonClickedHandler : IGlobalSubscriber
    {
        public void OnPauseButtonClicked();
    }
}
