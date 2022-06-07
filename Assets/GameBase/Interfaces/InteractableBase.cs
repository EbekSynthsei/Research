using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaniakeaCode.Utilities
{
    public class InteractableBase : MonoBehaviour, IInteractable
    {
        [Header("Interaction Settings")]
        private float holdDuration;
        private bool holdInteract;
        private bool multipleUse;
        private bool isInteractable;

        public float HoldDuration { get => holdDuration; set => holdDuration = value; }
        public bool HoldInteract { get => holdInteract; set => holdInteract = value; }
        public bool MultipleUse { get => multipleUse; set => multipleUse = value; }
        public bool IsInteractable { get => isInteractable; set => isInteractable = value; }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void OnInteract()
        {
            Debug.Log("Interacted with : " + gameObject.name);
        }
    }
}