﻿using System.Collections.Generic;
using LaniakeaCode.Utilities;

namespace LaniakeaCode.Events
{

    public abstract class BaseGameEvent<T> : ScriptableEvent
    {
        private readonly List<IGameEventListener<T>> eventListener = new List<IGameEventListener<T>>();
        
        public void Raise(T item) 
        {
            for (int i = eventListener.Count - 1; i >= 0; i--)
            {
                eventListener[i].OnEventRaised(item);
            }
        }
        public void RegisterListener(IGameEventListener<T> listener)
        {
            if (!eventListener.Contains(listener))
            {
                eventListener.Add(listener);
            }
        }

        public void UnregisterListener(IGameEventListener<T> listener)
        {
            if (eventListener.Contains(listener))
            {
                eventListener.Remove(listener);
            }
        }
    }
}