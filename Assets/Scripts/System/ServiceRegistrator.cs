using BattleSystem;
using UISystem;
using UnityEngine;

public class ServiceRegistrator : MonoBehaviour
{
    private void Awake()
    {
        new ServiceLocator();
        ServiceLocator.RegisterService(new BattleService());
        ServiceLocator.RegisterService(new GameHost());
        ServiceLocator.RegisterService(new AudioService());
        ServiceLocator.RegisterService(new UIService());
    }
}
