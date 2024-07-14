using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LaniakeaCode.Events;

namespace LaniakeaCode.Utilities
{
    public interface IInteractable
    {
        float HoldDuration { get; }
        bool HoldInteract { get; }
        bool MultipleUse { get; }
        bool IsInteractable { get; }
        bool AgentInInteractionArea();
        void OnInteract();
        void SetInteractionArea();
    }
}