using Cinemachine;
using LaniakeaCode.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        private bool debugEnabled;
        [SerializeField]
        private bool isInteractable;

        [Header("Camera")]
        [Tooltip("Should be the Interactable Camera")]
        [SerializeField]
        Cinemachine.CinemachineVirtualCamera virtualCamera;

        [Header("Ui For Interaction Tooltip")]
        [SerializeField]
        private RectTransform uiElement;

        public float HoldDuration { get => interactionData.holdDuration; set => holdDuration = interactionData.holdDuration; }
        public bool HoldInteract { get => interactionData.holdInteract; set => holdInteract = interactionData.holdInteract; }
        public bool MultipleUse { get => interactionData.multipleUse; set => multipleUse = interactionData.multipleUse; }
        public bool IsInteractable { get => isInteractable; set => isInteractable = value; }


        void Awake()
        {
            ValidateInteractable();
            SetInteractionArea();
            SetUITarget();
        }

        private void ValidateInteractable()
        {
            if(interactionData == null)
            {
                Debug.LogError("Nessuna Interazione su " + gameObject.name);
            }
            if (uiElement == null)
            {
                Debug.LogError("Nessuna UI D'Interazione su " + gameObject.name);
            }
        }

        private void SetUITarget()
        {
            
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
                if (debugEnabled)
                {
                    Debug.Log("In Area : " + gameObject.name);
                }
                ShowInteractionHint(true);
                OnInteract();
            }
        }
        public void OnInteract()
        {
            if (isInteractable && AgentInArea())
            {
                if (debugEnabled)
                {
                    if (interactionData.scriptableActions.Count != 0)
                    {
                        interactionData.scriptableActions.
                            ForEach(x => x
                            .PerformAction(this.gameObject, this.gameObject)); //TODO: SPECIFICARE AGENTE
                    }
                }
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
                ShowInteractionHint(true);
                BoolEvent x = interactionData.scriptableEvent;
                x.Raise();
            }
            
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                ShowInteractionHint(false);
                BoolEvent x = interactionData.scriptableEvent;
                x.Raise(false);
            }

        }
        public void ShowInteractionHint(bool shouldShow)
        {
            RectTransform CanvasRect = GetComponentInChildren<RectTransform>();
            if (debugEnabled)
            {
                Debug.Log("HO MOSTRATO L'HINT" + interactionData.interactionName);
                
            }
            Vector2 ViewportPosition = Camera.main.transform.position;
            Debug.Log("Viewport Position x:" + ViewportPosition.x + " y: " + ViewportPosition.y);

            Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));
            
            //now you can set the position of the ui element
            uiElement.anchoredPosition = WorldObject_ScreenPosition;
        }
#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            if (interactionData != null)
            {
                SetInteractionArea();
                Gizmos.color = Color.magenta;
                Gizmos.DrawWireSphere(interactionPoint.transform.position, interactionData.interactionAreaRadius);
                if (debugEnabled)
                {
                    Debug.Log("Interaction area test point :" + interactionPoint.ToString() + "radius:" + interactionArea.radius + "data:" + interactionData.interactionName);
                }
            }
        }
#endif
    }
}