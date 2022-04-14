using System;
using System.Collections.Generic;
using UCARPG.Domain.Standard.UseCases;

namespace UCARPG.Utilities
{
    public class EventBus : IEventPublisher
    {
        private Dictionary<Type, List<Action<object>>> _callbackListsByType = new Dictionary<Type, List<Action<object>>>();

        public void Register<T>(Action<T> callback)
        {
            Type type = typeof(T);
            if (!_callbackListsByType.ContainsKey(type))
            {
                _callbackListsByType[type] = new List<Action<object>>();
            }
            _callbackListsByType[type].Add(signal => callback((T)signal));
        }

        public void Post(object signal)
        {
            Type type = signal.GetType();
            if (!_callbackListsByType.ContainsKey(type))
            {
                return;
            }
            foreach (var callback in _callbackListsByType[type])
            {
                callback(signal);
            }
        }

        public void PostAll(List<object> signals)
        {
            List<object> signalsCopy = new List<object>(signals);
            signals.Clear();
            foreach (var signal in signalsCopy)
            {
                Post(signal);
            }
        }
    }
}