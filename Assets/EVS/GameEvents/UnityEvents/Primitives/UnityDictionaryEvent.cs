using UnityEngine.Events;
using LaniakeaCode.Utilities;
using System.Collections.Generic;

namespace LaniakeaCode.Events
{
    /// <summary>
    /// Unity event that takes a Dictionary of Entity and IInteractable arguments.
    /// </summary>
    [System.Serializable] public class UnityDictionaryEvent : UnityEvent<Dictionary<Entity, IInteractable>> { }
}
