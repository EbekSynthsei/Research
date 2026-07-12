using System.Collections.Generic;
using UnityEngine;
using LaniakeaCode.Events;
using LaniakeaCode.GraphSystem;

namespace LaniakeaCode.Utilities
{
    [CreateAssetMenu(fileName = "Interactable Data", menuName = "LaniakeaTools/Interactions")]
    public class InteractableData : ScriptableTypeData
    {
        [Header("Interaction")]
        public string interactionName;
        public LayerMask validAgents;

        [Range(0f, 10f)]
        public float interactionAreaRadius;
        [Range(0.2f, 20f)]
        public float focusAreaRadius;
        public Transform focusCenter;

        [Min(0f)]
        public float holdDuration;
        public bool holdInteract;
        public bool multipleUse;

        [Header("Dialogue")]
        public GraphTree dialogueGraph; // NEW: dialogo specifico per questo interactable

        [Header("Interaction Actions")]
        public List<ScriptableAction> scriptableActions;

        [Header("Interaction Events")]
        public BoolEvent scriptableEvent;

        public virtual void Interact() { }
    }
}