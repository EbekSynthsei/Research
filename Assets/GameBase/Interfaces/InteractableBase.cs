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
        private CircleCollider2D focusArea;

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
        [SerializeField]
        private bool isFocusOnPlayerInArea = false;
        [SerializeField]
        private GameObject focusTarget;

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
            SetFocusTarget();
            SetFocusArea();
        }


        private void ValidateInteractable()
        {
            if (interactionData == null)
            {
                Debug.LogError("Nessuna Interazione su " + gameObject.name);
            }
            if (uiElement == null)
            {
                Debug.LogError("Nessuna UI D'Interazione su " + gameObject.name);
            }
        }

        public void SetInteractionArea()
        {
            interactionPoint = gameObject.transform;
            interactionArea = GetComponent<CircleCollider2D>();
            interactionArea.isTrigger = true;
            interactionArea.radius = interactionData.interactionAreaRadius;
            interactionArea.transform.position = interactionPoint.position;
        }

        private void SetFocusTarget()
        {
            if (focusTarget == null && interactionData.focusCenter==null)
            {
                Debug.LogWarning("No Focus Target Set, defaulting to self");
                focusTarget = this.gameObject;
            }
            else if(focusTarget!= null || interactionData.focusCenter!= null)
            {
                if (interactionData.focusCenter != null)
                {
                    focusTarget = interactionData.focusCenter.gameObject;
                }
            }
        }

        private void SetFocusArea()
        {
            focusArea = (CircleCollider2D)this
                .gameObject
                .AddComponent(typeof(CircleCollider2D));
            focusArea
                .isTrigger = true;
            focusArea
                .radius = interactionData.focusAreaRadius;
            focusArea
                .transform
                .position = interactionData
                .focusCenter != null ? interactionData
                .focusCenter
                .position 
                : focusTarget
                .transform
                .position;
        }

        // Update is called once per frame
        void Update()
        {
            if (AgentInInteractionArea())
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
            if (isInteractable && AgentInInteractionArea())
            {
                if (debugEnabled)
                {
                    interactionData.Interact();
                }
            }
        }

        public bool AgentInInteractionArea()
        {
            return interactionArea.IsTouchingLayers(interactionData.validAgents); //TODO : MANAGE VALID AGENTS AND SWITCH BEHAVIOUR
        }



        private void SwitchFocus(bool focus)
        {
            virtualCamera.Follow = focusTarget.transform;

            if (isFocusOnPlayerInArea)
            {
                BoolEvent x = interactionData.scriptableEvent;
                x.Raise();
            }
        }
        private Type CheckPlayerState(Collider2D collision)
        {
            Player targetPlayer = collision.gameObject.GetComponent<Player>();
            return targetPlayer.stateMachine.currentState.GetType();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.tag == "Player" && collision.gameObject.tag == "Barrel")
            {
                SwitchFocus(true);
                ShowInteractionHint(true);
            }

        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (typeof(O_AttackState) == CheckPlayerState(collision))
            {
                OnInteract(); //TODO: MANAGE THE EVENT
            }
        }



        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                SwitchFocus(false);
                ShowInteractionHint(false);

            }

        }
        public void ShowInteractionHint(bool shouldShow)
        {
            RectTransform CanvasRect = GetComponentInChildren<RectTransform>(); //TODO : DELEGATE THIS TO A UI MANAGER
            Vector2 ViewportPosition = Camera.main.transform.position;
            if (debugEnabled)
            {
                Debug.Log("HO MOSTRATO L'HINT" + interactionData.interactionName);
                Debug.Log("Viewport Position x:" + ViewportPosition.x + " y: " + ViewportPosition.y);
            }

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
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(focusTarget.transform.position, interactionData.focusAreaRadius);
                if (debugEnabled)
                {
                    Debug.Log("Interaction area test point :" + interactionPoint.ToString() + "radius:" + interactionArea.radius + "data:" + interactionData.interactionName);
                }
            }
        }
#endif
    }
}