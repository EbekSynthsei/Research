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
        private RectTransform uiElement;
        Cinemachine.CinemachineVirtualCamera virtualCamera;
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
            RectTransform CanvasRect = GetComponentInChildren<RectTransform>();
            
            Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(transform.position);
            Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

            //now you can set the position of the ui element
            uiElement.anchoredPosition = WorldObject_ScreenPosition;
            Debug.Log(WorldObject_ScreenPosition);
        }
#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            if (interactionData != null)
            {
                SetInteractionArea();
                Gizmos.color = Color.magenta;
                Gizmos.DrawWireSphere(interactionPoint.transform.position, interactionData.interactionAreaRadius);
            }
        }
#endif
    }
}