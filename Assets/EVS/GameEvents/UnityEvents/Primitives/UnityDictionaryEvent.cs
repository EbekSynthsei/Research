using UnityEngine.Events;
using LaniakeaCode.Utilities;
using System.Collections.Generic;

namespace LaniakeaCode.Events
{
    [System.Serializable] public class UnityDictionaryEvent : UnityEvent<Dictionary<Entity, IInteractable>> { }
}
