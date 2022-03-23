using UnityEngine;

namespace LaniakeaCode.Events
{
    [CreateAssetMenu(fileName = "New Void Event", menuName = "LaniakeaScriptable/EventSystem/VoidEvent" )]
    public class VoidEvent : BaseGameEvent<Void>
    {
        public void Raise() => Raise(new Void());
    }
} 