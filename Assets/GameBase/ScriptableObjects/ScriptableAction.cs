using UnityEngine;
using System.Diagnostics.CodeAnalysis;
namespace LaniakeaCode.Utilities
{
    public abstract class ScriptableAction : ScriptableObject
    {
        public abstract void PerformAction(GameObject agent, GameObject subject);
    }
}
