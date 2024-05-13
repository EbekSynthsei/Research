using System.Collections.Generic;
using UnityEngine;
using LaniakeaCode.Utilities;

namespace LaniakeaCode.Events
{
    [CreateAssetMenu(fileName = "New Dictionary Event", menuName = "LaniakeaScriptable/EventSystem/Primitives/DictionaryEvent")]
    public class DictionaryEvent : BaseGameEvent<Dictionary<Entity, IInteractable>>
    {

    }
}
