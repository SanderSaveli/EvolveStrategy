using EventBusSystem;
using System.Collections.Generic;

public class GameHost : Singletone<GameHost>, IAcktorDiedHandler
{
    private Dictionary<AcktorList, GameAcktor> acktiveAcktors = new();
    private List<BattleBot> bots = new();
    private bool _isGameEnd;

    private void Start()
    {
        EventBus.Subscribe(this);
    }

    public GameAcktor GetAcktorByEnum(AcktorList acktor)
    {
        if (!acktiveAcktors.ContainsKey(acktor))
        {
            acktiveAcktors.Add(acktor, CreateAcktor(acktor));
        }
        return acktiveAcktors[acktor];
    }

    private GameAcktor CreateAcktor(AcktorList acktor)
    {
        Bank.instance.OpenAnAccount(acktor, 0);
        switch (acktor)
        {
            case AcktorList.Player:
                return new Player();
            case AcktorList.None:
                return new NoneAcktor();
            default:
                BattleBot bot = new BattleBot(acktor);
                bots.Add(bot);
                bot.StartBot();
                return bot;
        }
    }
    public void AcktorDie(GameAcktor acktor)
    {
        if (!_isGameEnd)
        {
            if (acktor.acktorName == AcktorList.Player)
            {
                _isGameEnd = true;
                EventBus.RaiseEvent<IGameEndHandler>(it => it.PlayerLose());
            }
            else if (acktor.acktorName != AcktorList.None)
            {
                BattleBot bot = acktor as BattleBot;
                if (bots.Contains(bot))
                {
                    bots.Remove(bot);
                    bot.StopBot();
                    if (bots.Count == 0)
                    {
                        _isGameEnd = true;
                        EventBus.RaiseEvent<IGameEndHandler>(it => it.PlayerWin());
                    }
                }
            }
        }
    }
}
