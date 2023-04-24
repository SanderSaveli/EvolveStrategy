using EventBusSystem;
using UnityEngine;

public class Pause : MonoBehaviour, IPauseButtonClickedHandler
{
    public RectTransform pauseMenu;
    public RectTransform pauseButton;
    public GameStateManager gameStateManager;
    private void Start()
    {
        gameStateManager = GameStateManager.instance;
        EventBus.Subscribe(this);
    }

    public void OnPauseButtonClicked()
    {
        OpenPause();
    }

    private void OpenPause()
    {
        if(gameStateManager.currentState!= GameStates.NestCellChoses) 
        {
            pauseMenu.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);
            EventBus.RaiseEvent<IPauseMenuEventHandler>(it => it.OpenPause());
        }
    }
    private void ClousePause()
    {
        pauseMenu.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        EventBus.RaiseEvent<IPauseMenuEventHandler>(it => it.ClousePause());
    }

    public void BackToMenu()
    {
        EventBus.RaiseEvent<IPauseMenuEventHandler>(it => it.BackToMenu());
    }

    public void Restartlevel()
    {
        EventBus.RaiseEvent<IPauseMenuEventHandler>(it => it.Restart());
    }

}
