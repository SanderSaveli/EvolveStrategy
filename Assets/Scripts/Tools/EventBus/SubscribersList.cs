using System.Collections.Generic;

public class SubscribersList<TSubscriber> where TSubscriber : class
{
    private bool _needsCleanUp = false;

    public bool _isExecuting;

    public readonly List<TSubscriber> List = new List<TSubscriber>();

    public void Add(TSubscriber subscriber)
    {
        List.Add(subscriber);
    }

    public void Remove(TSubscriber subscriber)
    {
        if (_isExecuting)
        {
            var i = List.IndexOf(subscriber);
            if (i >= 0)
            {
                _needsCleanUp = true;
                List[i] = null;
            }
        }
        else
        {
            List.Remove(subscriber);
        }
    }

    public void Cleanup()
    {
        if (!_needsCleanUp)
        {
            return;
        }

        List.RemoveAll(s => s == null);
        _needsCleanUp = false;
    }
}
