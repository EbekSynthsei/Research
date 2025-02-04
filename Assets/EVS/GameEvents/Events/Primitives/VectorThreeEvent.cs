using UnityEngine;

namespace LaniakeaCode.Events
{
    /// <summary>
    /// Event class for events that pass a Vector3 parameter.
    /// </summary>
    [CreateAssetMenu(fileName = "New VectorThree Event", menuName = "LaniakeaScriptable/EventSystem/Primitives/Vector/Vector3")]
    public class VectorThreeEvent : BaseGameEvent<Vector3>
    {
    }
}