using LaniakeaCode.Events;
using LaniakeaCode.GraphSystem;
using UnityEngine;

namespace LaniakeaCode.Events
{
    [CreateAssetMenu(menuName = "LaniakeaScriptable/Graph/GraphEvent")]
    public class GraphEvent : BaseGameEvent<bool>, IRaisable
    {
        public void Raise() => Raise(true);
    }
}