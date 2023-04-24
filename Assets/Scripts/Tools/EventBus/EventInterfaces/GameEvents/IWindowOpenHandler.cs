namespace EventBusSystem 
{ 
    public interface IWindowOpenHandler : IGlobalSubscriber
    {
        public void WindowOnen();
        public void WindowClosed();
    }
}


