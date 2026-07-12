using System;
using UnityEngine;
using LaniakeaCode.Events;
using Cinemachine;

namespace LaniakeaCode.Utilities
{
    public class CameraBehaviourManager : MonoBehaviour
    {
        [Header("Cinemachine")]
        [SerializeField] private CinemachineVirtualCamera poiCamera;
        [SerializeField] private float defaultPOIReturnDelay = 2f;

        private Animator animator;
        private StringListener listener;
        private BoolListener boolListener;
        private CameraBehaviour currentBehaviour = CameraBehaviour.FollowPlayer;
        private Coroutine poiCoroutine;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            boolListener = GetComponent<BoolListener>();
            listener = GetComponent<StringListener>();
        }

        // Chiamato da EVS con stringa (compatibile con quello che hai)
        public void SwitchState(string behaviour)
        {
            var parsed = Enum.TryParse<CameraBehaviour>(behaviour, true, out var result)
                ? result
                : CameraBehaviour.FollowPlayer;
            SwitchState(parsed);
        }

        public void SwitchState(CameraBehaviour behaviour)
        {
            if (currentBehaviour == behaviour) return;
            currentBehaviour = behaviour;

            switch (behaviour)
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

        // Nuovo: focus su POI con target dinamico e ritorno automatico
        public void FocusPOI(Transform target, float holdTime = -1f)
        {
            if (poiCamera == null) return;
            if (poiCoroutine != null) StopCoroutine(poiCoroutine);

            poiCamera.Follow = target;
            poiCamera.LookAt = target;
            SwitchState(CameraBehaviour.FollowTarget);

            float duration = holdTime < 0 ? defaultPOIReturnDelay : holdTime;
            poiCoroutine = StartCoroutine(ReturnToPlayerAfter(duration));
        }

        // Chiamabile anche da EVS per ritorno manuale (es. fine dialogo)
        public void ReturnToPlayer()
        {
            if (poiCoroutine != null) StopCoroutine(poiCoroutine);
            SwitchState(CameraBehaviour.FollowPlayer);
        }

        private System.Collections.IEnumerator ReturnToPlayerAfter(float delay)
        {
            yield return new WaitForSeconds(delay);
            ReturnToPlayer();
        }
    }
}