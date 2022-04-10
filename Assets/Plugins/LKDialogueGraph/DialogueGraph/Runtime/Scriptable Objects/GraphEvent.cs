using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaniakeaCode.GraphSystem
{
    /// <summary>
    /// The Basic Scriptable Object Event. Performs A Raise
    /// TODO : Merge with Code of Events
    /// </summary>
    [CreateAssetMenu(menuName = "LaniakeaScriptable/Graph/GraphEvent")]
    public class GraphEvent : ScriptableObject
    {
        public virtual void Raise()
        {

        }
    }
}