using EventBusSystem;
using System;
using System.Collections.Generic;

public class Bank : Singletone<Bank>
{
    private Dictionary<AcktorList, int> _playersPoints = new();
    public bool OpenAnAccount(AcktorList acktor, int startPoints) 
    {
        if (_playersPoints.ContainsKey(acktor))
        {
            return false;
        }
        _playersPoints.Add(acktor, startPoints);
        return false;
    }
    public bool TryToBuy(AcktorList acktor, int cost)
    {
        if (cost < 0)
        {
            throw new Exception("The price cannot be negative");
        }
        if (_playersPoints[acktor] - cost > 0)
        {
            _playersPoints[acktor] -= cost;
            EventBus.RaiseEvent<IEvolvePointsChangeHandler>(it => it.EvolvePointsChanges(acktor, _playersPoints[acktor]));

            return true;
        }
        return false;
    }

    public void AddPoints(AcktorList acktor, int value)
    {
        _playersPoints[acktor] += value;
        EventBus.RaiseEvent<IEvolvePointsChangeHandler>(it => it.EvolvePointsChanges(acktor, value));
    }

    public int GetAcktorPoints(AcktorList acktor) 
    {
        return _playersPoints[acktor];
    }
}
