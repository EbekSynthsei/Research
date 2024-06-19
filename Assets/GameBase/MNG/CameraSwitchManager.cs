using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using LaniakeaCode.Events;

namespace LaniakeaCode.Utilities
{
    class CameraSwitchManager : MonoBehaviour
    {

        private Animator animator;

        private bool mainCamera = false;

        private BoolListener BoolListener;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            BoolListener = GetComponent<BoolListener>();
        }

        public void SwitchState(bool _mainCamera)
        {
            

            if (mainCamera!= _mainCamera)
            {
                animator.Play("PlayerCamera");
            }
            else
            {
                animator.Play("Interactable Camera");
            }
            mainCamera = !mainCamera;

            Debug.Log("Received " + mainCamera);
        }

    }
}
