using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaniakeaCode.Utilities
{
    public interface IInteractable
    {
        float HoldDuration { get; }
        bool HoldInteract { get; }
        bool MultipleUse { get; }
        bool IsInteractable { get; }
        bool AgentInArea();
        void OnInteract();
        void SetInteractionArea();
    }
}