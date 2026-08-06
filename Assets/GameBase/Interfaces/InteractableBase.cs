using Unity.Cinemachine;
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
        [Tooltip("Prefab world-space con Canvas (Render Mode = World Space) + Image (frame) " +
                 "+ TextMeshProUGUI (es. \"E\" o \"Interact\"). Instanziato come figlio del " +
                 "focusTarget, così segue l'oggetto senza bisogno di codice di posizionamento manuale.")]
        [SerializeField] private GameObject interactionTooltipPrefab;

        [Tooltip("Offset locale del tooltip rispetto al focusTarget (es. sopra la testa dell'NPC).")]
        [SerializeField] private Vector3 tooltipOffset = new Vector3(0f, 1.5f, 0f);

        private GameObject tooltipInstance;

        // In Awake(), dopo SetFocusTarget(), istanzia il tooltip (disattivato):
        private void SetupTooltip()
        {
            if (interactionTooltipPrefab == null) return;

            tooltipInstance = Instantiate(interactionTooltipPrefab, focusTarget.transform);
            tooltipInstance.transform.localPosition = tooltipOffset;
            tooltipInstance.SetActive(false);
        }

        public void ShowInteractionHint(bool shouldShow)
        {
            if (tooltipInstance == null) return;
            tooltipInstance.SetActive(shouldShow);
        }
    
        public float HoldDuration => interactionData.holdDuration;
        public bool HoldInteract => interactionData.holdInteract;
        public bool MultipleUse => interactionData.multipleUse;
        public bool IsInteractable { get => isInteractable; set => isInteractable = value; }

        private void Awake()
        {
            ValidateInteractable();
            SetInteractionArea();
            SetFocusTarget();
            SetupTooltip();
            SetFocusArea();
        }

        private void ValidateInteractable()
        {
            if (interactionData == null)
                Debug.LogError("Nessuna Interazione", this);
            if (interactionTooltipPrefab == null)
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
                Debug.Log("InteractableBase: Starting dialogue with graph : " + interactionData.dialogueGraph.name, this);
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

            // Close and reset active dialogue when player leaves interaction area
            CloseActiveDialogue();
        }

        private void CloseActiveDialogue()
        {
            var dc = FindAnyObjectByType<DialogueController>();
            dc?.ForceClose();
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