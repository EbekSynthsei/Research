using Cinemachine;
using LaniakeaCode.Events;
using System;
using UnityEngine;

namespace LaniakeaCode.Utilities
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class InteractableBase : MonoBehaviour, IInteractable
    {
        private CircleCollider2D interactionArea;
        private CircleCollider2D focusArea;
        private Transform interactionPoint;
        private Entity currentInteractor;
        private bool playerInRange;

        [Header("Interaction Settings")]
        [SerializeField] private InteractableData interactionData;

        [SerializeField] private bool debugEnabled;
        [SerializeField] private bool isInteractable = true;

        [Header("Camera")]
        [SerializeField] private CameraBehaviourManager cameraManager;
        [SerializeField] private GameObject focusTarget;

        [Header("Ui For Interaction Tooltip")]
        [SerializeField] private RectTransform uiElement;

        public float HoldDuration => interactionData.holdDuration;
        public bool HoldInteract => interactionData.holdInteract;
        public bool MultipleUse => interactionData.multipleUse;
        public bool IsInteractable { get => isInteractable; set => isInteractable = value; }

        private void Awake()
        {
            ValidateInteractable();
            SetInteractionArea();
            SetFocusTarget();
            SetFocusArea();
        }

        private void ValidateInteractable()
        {
            if (interactionData == null)
                Debug.LogError("Nessuna Interazione", this);
            if (uiElement == null)
                Debug.LogError("Nessuna UI D'Interazione", this);
        }

        public void SetInteractionArea()
        {
            interactionPoint = transform;
            interactionArea = GetComponent<CircleCollider2D>();
            interactionArea.isTrigger = true;
            interactionArea.radius = interactionData.interactionAreaRadius;
        }

        private void SetFocusTarget()
        {
            if (focusTarget == null)
                focusTarget = interactionData.focusCenter != null
                    ? interactionData.focusCenter.gameObject
                    : gameObject;
        }

        private void SetFocusArea()
        {
            focusArea = gameObject.AddComponent<CircleCollider2D>();
            focusArea.isTrigger = true;
            focusArea.radius = interactionData.focusAreaRadius;
        }

        // ── IInteractable — punto unico di ingresso, niente più duplicati ──
       public void OnInteract(Entity interactor)
        {
            Debug.Log("InteractableBase: OnInteract called", this);
            if (!isInteractable || interactor != currentInteractor)
            {
                Debug.Log("InteractableBase: OnInteract returning early - isInteractable=" + isInteractable + ", interactor matches current=" + (interactor == currentInteractor), this);
                return;
            }

            interactionData.Interact();

            foreach (var action in interactionData.scriptableActions)
                action?.PerformAction(interactor.gameObject, gameObject);

            if (interactionData.dialogueGraph != null)
            {
                Debug.Log("InteractableBase: Starting dialogue with graph", this);
                var dc = FindAnyObjectByType<DialogueController>();
                dc?.StartUIPanel(interactionData.dialogueGraph);
            }

            interactionData.scriptableEvent?.Raise();

            if (!interactionData.multipleUse)
                isInteractable = false;
        }

       public void OnFocus(Entity interactor)
        {
            currentInteractor = interactor;
            playerInRange = true;
            ShowInteractionHint(true);

            if (interactor is Player p)
                p.CurrentFocusedInteractable = this;

            if (cameraManager != null && focusTarget != null)
                cameraManager.FocusPOI(focusTarget.transform);
        }

        public void OnLoseFocus(Entity interactor)
        {
            if (interactor != currentInteractor) return;
            playerInRange = false;
            currentInteractor = null;
            ShowInteractionHint(false);

            if (interactor is Player p && p.CurrentFocusedInteractable == this)
                p.CurrentFocusedInteractable = null;

            cameraManager?.ReturnToPlayer();
        }

        // ── Trigger fisico — sostituisce il polling in Update ──
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("InteractableBase: OnTriggerEnter2D called", this);
            if (other.TryGetComponent<Entity>(out var entity))
                OnFocus(entity);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            Debug.Log("InteractableBase: OnTriggerStay2D called", this);
            if (other.TryGetComponent<Entity>(out var entity))
                OnFocus(entity);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log("InteractableBase: OnTriggerExit2D called", this);
            if (other.TryGetComponent<Entity>(out var entity))
                OnLoseFocus(entity);
        }

        // Chiamato dal PlayerInputHandler quando preme il tasto interact
        public void TryInteract()
        {
            Debug.Log("InteractableBase: TryInteract called. playerInRange=" + playerInRange + ", currentInteractor != null=" + (currentInteractor != null), this);
            if (playerInRange && currentInteractor != null)
            {
                Debug.Log("InteractableBase: Calling OnInteract", this);
                OnInteract(currentInteractor);
            }
        }

        public void ShowInteractionHint(bool shouldShow)
        {
            if (uiElement == null) return;
            uiElement.gameObject.SetActive(shouldShow);
            // TODO: delegare posizionamento a un UI Manager dedicato
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (interactionData == null || interactionPoint == null) return;
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(interactionPoint.position, interactionData.interactionAreaRadius);
            Gizmos.color = Color.blue;
            if (focusTarget != null)
                Gizmos.DrawWireSphere(focusTarget.transform.position, interactionData.focusAreaRadius);
        }
#endif
    }
}