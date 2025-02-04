using UnityEngine;

namespace LaniakeaCode.Events
{
    /// <summary>
    /// Event class for events that pass a bool parameter.
    /// </summary>
    [CreateAssetMenu(fileName = "New Bool Event", menuName = "LaniakeaScriptable/EventSystem/Primitives/BoolEvent")]
    public class BoolEvent : BaseGameEvent<bool>
    {
        /// <summary>
        /// Raises the event with a false parameter.
        /// </summary>
        public void Raise() => Raise(false);

        /// <summary>
        /// Raises the event with a true parameter.
        /// </summary>
        public void RaiseTrue() => Raise(true);
    }
}