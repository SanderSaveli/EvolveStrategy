using EventBusSystem;
using TileSystem;

public class GameStateManager : IService, IPlayerChoosesNestCellHandler, IWindowOpenHandler, IGameEndHandler
{
    public GameStates currentState { get; private set; }

    public GameStateManager(GameStates startState) 
    { 
        currentState = startState;
    }
    public void StartWork()
    {
        currentState = GameStates.Battle;
        EventBus.Subscribe(this);
    }

    public void EndWork()
    {

    }

    public void StartChoiseState(Region region)
    {
        ChangeCurrentState(GameStates.NestCellChoses);
    }
    public void EndChoiseState(Region region)
    {
        ChangeCurrentState(GameStates.Battle);
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
