using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using LaniakeaCode.Events;

namespace LaniakeaCode.Utilities
{
    public class CameraBehaviourManager : MonoBehaviour
    {

        private Animator animator;

        private bool mainCamera = false;
        private StringListener listener;

        private BoolListener BoolListener;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            BoolListener = GetComponent<BoolListener>();
            listener = GetComponent<StringListener>();
            
        }
        private void Start()
        {
            
        }
        private void Update()
        {
            
        }
        public void SwitchState(string behaviour)
        {
            CameraBehaviour cameraBehaviour = Enum.TryParse<CameraBehaviour>(behaviour, true, out cameraBehaviour) ? cameraBehaviour : CameraBehaviour.FollowPlayer;

            switch (cameraBehaviour)
            {
                case CameraBehaviour.FollowTarget:
                    animator.Play("Interactable Camera");
                    break;
                case CameraBehaviour.FollowPlayer:
                default:
                    animator.Play("PlayerCamera");
                    break;
            }
        }
        public void SwitchState(bool _mainCamera)
        {

            Debug.LogWarning("Received" + _mainCamera);
            if (mainCamera!= _mainCamera)
            {
                animator.Play("PlayerCamera");
            }
            else
            {
                animator.Play("Interactable Camera");
            }
            mainCamera = !mainCamera;

            Debug.LogWarning("Received out" + mainCamera);
        }

    }
}
