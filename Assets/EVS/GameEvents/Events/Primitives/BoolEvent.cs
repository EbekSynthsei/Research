using UnityEngine;

namespace LaniakeaCode.Events
{
    [CreateAssetMenu(fileName = "New Bool Event", menuName = "LaniakeaScriptable/EventSystem/Primitives/BoolEvent")]
    public class BoolEvent : BaseGameEvent<bool>
    {
        public void Raise() => Raise(false);
        public void RaiseTrue() => Raise(true);
    }
}