using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaniakeaCode.Utilities;

namespace LaniakeaCode.Events
{
    public class InteractableMapListener: BaseGameEventListener<Dictionary<Entity,IInteractable>, DictionaryEvent, UnityDictionaryEvent>
    {

    }
}