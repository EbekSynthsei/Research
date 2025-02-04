namespace LaniakeaCode.Events
{
    /// <summary>
    /// Interface for game event listeners.
    /// </summary>
    /// <typeparam name="T">Type of the event item.</typeparam>
    public interface IGameEventListener<T>
    {
        /// <summary>
        /// Method to be called when the event is raised.
        /// </summary>
        /// <param name="item">The event item.</param>
        void OnEventRaised(T item);
    }
}