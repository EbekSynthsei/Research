using UnityEngine;

namespace LaniakeaCode.Events
{
    [CreateAssetMenu(fileName = "New String Event", menuName = "LaniakeaScriptable/EventSystem/Primitives/StringEvent")]
    public class StringEvent : BaseGameEvent<string>
    {
        [SerializeField]
        private string ToPass = "";
        public void Raise() => Raise(ToPass);
    }
}