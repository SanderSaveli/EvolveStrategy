using BattleSystem;
using UISystem;
using UnityEngine;

public class ServiceRegistrator
{
    public void RegistrateAllServices()
    {
        new ServiceLocator();
        ServiceLocator.RegisterService(new GameStateManager(GameStates.Battle));
        ServiceLocator.RegisterService(new BattleService());
        ServiceLocator.RegisterService(new GameHost());
        ServiceLocator.RegisterService(new AudioService());
        ServiceLocator.RegisterService(new UIService());
    }
}
