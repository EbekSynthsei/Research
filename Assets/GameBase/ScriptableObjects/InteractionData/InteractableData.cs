using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaniakeaCode.Utilities
{
    [CreateAssetMenu(fileName = "Interactable Data", menuName = "Scriptable Data/Interactions")]
    public class InteractableData : ScriptableObject
    {
        [Header("Interaction")]
        public string interactionName;
        public LayerMask validAgents;

        [Range(0f, 10f)]
        public float interactionAreaRadius;

        [Min(0f)]
        public float holdDuration;
        public bool holdInteract;
        public bool multipleUse;
    }
}
