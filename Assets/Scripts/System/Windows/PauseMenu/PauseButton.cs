using EventBusSystem;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public void OnClicked()
    {
        EventBus.RaiseEvent<IPauseButtonClickedHandler>(it => it.OnPauseButtonClicked());
    }
}
