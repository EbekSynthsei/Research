using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaniakeaCode.Utilities;

namespace LaniakeaCode.Events
{
    /// <summary>
    /// Listener for events involving a dictionary of entities and interactables.
    /// </summary>
    public class InteractableMapListener : BaseGameEventListener<Dictionary<Entity, IInteractable>, DictionaryEvent, UnityDictionaryEvent>
    {

    }
}