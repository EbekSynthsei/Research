using UnityEngine;

namespace LaniakeaCode.Events
{
    /// <summary>
    /// Event class for events that pass an int parameter.
    /// </summary>
    [CreateAssetMenu(fileName = "New Int Event", menuName = "LaniakeaScriptable/EventSystem/Primitives/IntEvent" )]
    public class IntEvent : BaseGameEvent<int>
    {
    }
}