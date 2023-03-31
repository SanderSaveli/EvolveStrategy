using EventBusSystem;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public RectTransform pauseMenu;
    public RectTransform pauseButton;
    public GameStateManager gameStateManager;
    private void Start()
    {
        gameStateManager = GameStateManager.instance;
    }
    public void OpenPause()
    {
        if(gameStateManager.currentState!= GameStates.NestCellChoses) 
        {
            pauseMenu.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);
            EventBus.RaiseEvent<IPauseMenuEventHandler>(it => it.OpenPause());
        }
    }
    public void ClousePause()
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
