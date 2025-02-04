using UnityEngine;

namespace LaniakeaCode.Events
{
    /// <summary>
    /// Event class for events that pass a Vector2 parameter.
    /// </summary>
    [CreateAssetMenu(fileName = "New VectorTwo Event", menuName = "LaniakeaScriptable/EventSystem/Primitives/Vector/Vector2")]
    public class VectorTwoEvent : BaseGameEvent<Vector2>
    {
    }
}