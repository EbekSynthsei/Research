using UnityEngine;

namespace LaniakeaCode.Events
{
    /// <summary>
    /// Event class for events that do not require any parameters.
    /// </summary>
    [CreateAssetMenu(fileName = "New Void Event", menuName = "LaniakeaScriptable/EventSystem/VoidEvent" )]
    public class VoidEvent : BaseGameEvent<Void>
    {
        /// <summary>
        /// Raises the event with a new Void instance.
        /// </summary>
        public void Raise() => Raise(new Void());
    }
}