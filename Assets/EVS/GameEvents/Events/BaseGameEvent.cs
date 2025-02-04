using System.Collections.Generic;
using LaniakeaCode.Utilities;

namespace LaniakeaCode.Events
{
    /// <summary>
    /// Base class for game events that can be raised with a parameter of type T.
    /// </summary>
    public abstract class BaseGameEvent<T> : ScriptableEvent
    {
        private readonly List<IGameEventListener<T>> eventListener = new List<IGameEventListener<T>>();

        /// <summary>
        /// Raises the event with the specified parameter.
        /// </summary>
        /// <param name="item">The parameter to pass to the event listeners.</param>
        public void Raise(T item) 
        {
            for (int i = eventListener.Count - 1; i >= 0; i--)
            {
                eventListener[i].OnEventRaised(item);
            }
        }

        /// <summary>
        /// Registers a listener to the event.
        /// </summary>
        /// <param name="listener">The listener to register.</param>
        public void RegisterListener(IGameEventListener<T> listener)
        {
            if (!eventListener.Contains(listener))
            {
                eventListener.Add(listener);
            }
        }

        /// <summary>
        /// Unregisters a listener from the event.
        /// </summary>
        /// <param name="listener">The listener to unregister.</param>
        public void UnregisterListener(IGameEventListener<T> listener)
        {
            if (eventListener.Contains(listener))
            {
                eventListener.Remove(listener);
            }
        }
    }
}