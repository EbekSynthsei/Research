using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaniakeaCode.Utilities
{
    /// <summary>
    /// The component to add to an interactable Game Object
    /// </summary>
    [RequireComponent(typeof(CircleCollider2D))]
    public class InteractableBase : MonoBehaviour, IInteractable
    {
        private CircleCollider2D interactionArea;
        
        [Header("Interaction Settings")]
        [SerializeField]
        private InteractableData interactionData;

        private Transform interactionPoint;

        private float holdDuration;
        private bool holdInteract;
        private bool multipleUse;

        [SerializeField]
        private bool isInteractable;
        public float HoldDuration { get => interactionData.holdDuration; set => holdDuration = interactionData.holdDuration; }
        public bool HoldInteract { get => interactionData.holdInteract; set => holdInteract = interactionData.holdInteract; }
        public bool MultipleUse { get => interactionData.multipleUse; set => multipleUse = interactionData.multipleUse; }
        public bool IsInteractable { get => isInteractable; set => isInteractable = value; }

        void Awake()
        {
            SetInteractionArea();
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (AgentInArea())
            {
                ShowInteractionHint();
                OnInteract();
            }
        }
        public void OnInteract()
        {
            if (isInteractable)
            {
                Debug.Log("Interacted with : " + gameObject.name);
            }
        }

        public bool AgentInArea()
        {
            return interactionArea.IsTouchingLayers(interactionData.validAgents);
        }


        public void SetInteractionArea()
        {
            interactionPoint = gameObject.transform;
            interactionArea = GetComponent<CircleCollider2D>();
            interactionArea.isTrigger = true;
            interactionArea.radius = interactionData.interactionAreaRadius;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                ShowInteractionHint();

            }
            
        }
        public void ShowInteractionHint()
        {

        }
#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            if (interactionData != null)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawWireSphere(interactionPoint.transform.position, interactionData.interactionAreaRadius);
            }
        }
#endif
    }
}