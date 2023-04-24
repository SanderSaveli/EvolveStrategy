using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EventBusSystem
{ 
    public static class EventBus
    {
        private static Dictionary<Type, SubscribersList<IGlobalSubscriber>> _subscribers =
            new Dictionary<Type, SubscribersList<IGlobalSubscriber>>();

        private static Dictionary<Type, List<Type>> s_CashedSubscriberTypes =
            new Dictionary<Type, List<Type>>();

        public static void Subscribe(IGlobalSubscriber globalSubscriber)
        {
            List<Type> SubscriberTypes = GetSubscribersTypes(globalSubscriber);
            foreach (Type subscriberType in SubscriberTypes)
            {
                if (!_subscribers.ContainsKey(subscriberType))
                    _subscribers[subscriberType] = new SubscribersList<IGlobalSubscriber>();
                _subscribers[subscriberType].Add(globalSubscriber);
            }
        }

        public static void Unsubscribe(IGlobalSubscriber subcriber)
        {
            List<Type> subscriberTypes = GetSubscribersTypes(subcriber);
            foreach (Type t in subscriberTypes)
            {
                if (_subscribers.ContainsKey(t))
                    _subscribers[t].Remove(subcriber);
            }
        }

        public static void RaiseEvent<TSubscriber>(Action<TSubscriber> action)
        where TSubscriber : IGlobalSubscriber
        {
            if (_subscribers.ContainsKey(typeof(TSubscriber)))
            {
                SubscribersList<IGlobalSubscriber> subscribersOfThisAction = _subscribers[typeof(TSubscriber)];
                subscribersOfThisAction._isExecuting = true;

                foreach (IGlobalSubscriber subscriber in subscribersOfThisAction.List)
                {
                    action?.Invoke((TSubscriber)subscriber);
                    //try
                    //{
                    //    action?.Invoke((TSubscriber)subscriber);
                    //}
                    //catch (Exception e)
                    //{
                    //    Debug.LogError(e);
                    //}
                }

                subscribersOfThisAction._isExecuting = false;
                subscribersOfThisAction.Cleanup();
            }
        }

        public static List<Type> GetSubscribersTypes(IGlobalSubscriber globalSubscriber)
        {
            Type type = globalSubscriber.GetType();

            if (s_CashedSubscriberTypes.ContainsKey(type))
                return s_CashedSubscriberTypes[type];

            List<Type> subscriberTypes = type
                .GetInterfaces().Where(
                it => it.GetInterfaces().Contains(typeof(IGlobalSubscriber)))
                .ToList();

            s_CashedSubscriberTypes[type] = subscriberTypes;
            return subscriberTypes;
        }
    }
}

