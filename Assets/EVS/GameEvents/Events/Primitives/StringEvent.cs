using UnityEngine;

namespace LaniakeaCode.Events
{
    /// <summary>
    /// Event class for events that pass a string parameter.
    /// </summary>
    [CreateAssetMenu(fileName = "New String Event", menuName = "LaniakeaScriptable/EventSystem/Primitives/StringEvent")]
    public class StringEvent : BaseGameEvent<string>
    {
        [SerializeField]
        private string ToPass = "";

        /// <summary>
        /// Raises the event with the specified string parameter.
        /// </summary>
        public void Raise() => Raise(ToPass);
    }
}