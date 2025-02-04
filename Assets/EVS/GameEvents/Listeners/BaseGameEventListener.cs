using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LaniakeaCode.Events
{
    /// <summary>
    /// Base class for game event listeners.
    /// </summary>
    /// <typeparam name="T">Type of the event item.</typeparam>
    /// <typeparam name="E">Type of the game event.</typeparam>
    /// <typeparam name="UER">Type of the Unity event response.</typeparam>
    public abstract class BaseGameEventListener<T, E, UER> : MonoBehaviour,
        IGameEventListener<T> where E : BaseGameEvent<T> where UER : UnityEvent<T>
    {
        [SerializeField] private E gameEvent;
        /// <summary>
        /// The game event to listen to.
        /// </summary>
        public E GameEvent
        {
            get { return gameEvent; }
            set { gameEvent = value; }
        }
        [SerializeField] private UER unityEventResponse;

        private void OnEnable()
        {
            if (gameEvent == null) { return; }
            GameEvent.RegisterListener(this);
        }
        private void OnDisable()
        {
            if (gameEvent == null) { return; }

            GameEvent.UnregisterListener(this);
        }
        public void OnEventRaised(T item)
        {
            if (unityEventResponse != null)
            {
                unityEventResponse.Invoke(item);
            }
        }
    }
}