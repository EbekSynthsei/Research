using UnityEngine;

namespace LaniakeaCode.Events
{
    /// <summary>
    /// Event class for events that pass a float parameter.
    /// </summary>
    [CreateAssetMenu(fileName = "New Float Event", menuName = "LaniakeaScriptable/EventSystem/Primitives/FloatEvent")]
    public class FloatEvent : BaseGameEvent<float>
    {
    }
}