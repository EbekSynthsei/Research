using System.Collections.Generic;
using UnityEngine;
using LaniakeaCode.Utilities;

namespace LaniakeaCode.Events
{
    /// <summary>
    /// Event class for events that pass a dictionary of Entity and IInteractable parameters.
    /// </summary>
    [CreateAssetMenu(fileName = "New Dictionary Event", menuName = "LaniakeaScriptable/EventSystem/Primitives/DictionaryEvent")]
    public class DictionaryEvent : BaseGameEvent<Dictionary<Entity, IInteractable>>
    {

    }
}
