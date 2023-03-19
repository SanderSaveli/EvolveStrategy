using UnityEngine;
using EventBusSystem;
using TileSystem;

public class GameStateManager : Singletone<GameStateManager>, IPlayerChoosesNestCellHandler, IWindowOpenHandler, IGameEndHandler
{
    public GameStates currentState { get; private set; }

    public void StartState(Region region)
    {
        ChangeCurrentState(GameStates.NestCellChoses);
    }
    public void EndState(Region region)
    {
        ChangeCurrentState(GameStates.Battle);
    }

    private void Awake()
    {
        currentState = GameStates.Battle;
        EventBus.Subscribe(this);
    }

    private void ChangeCurrentState(GameStates newState)
    {
        if(currentState != newState) 
        {
            currentState = newState;
        }
    }

    public void WindowOnen()
    {
        ChangeCurrentState(GameStates.WindowOpen);
    }

    public void WindowClosed()
    {
        ChangeCurrentState(GameStates.Battle);
    }

    public void PlayerWin()
    {
        ChangeCurrentState(GameStates.GameEnd);
    }

    public void PlayerLose()
    {
        ChangeCurrentState(GameStates.GameEnd);
    }
}
