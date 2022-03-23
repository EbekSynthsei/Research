using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimToWeapon : MonoBehaviour
{
    private Weapon weapon;

    private void Start()
    {
        weapon = GetComponentInParent<Weapon>();
    }
    private void AnimationFinishTrigger()
    {
        weapon.AnimationFinishTrigger();
    }
    private void StartAnimationMovementTrigger()
    {
        weapon.AnimationStartMovementTrigger();
    }
    private void StopAnimationMovementTrigger()
    {
        weapon.AnimationStopMovementTrigger();
    }
    private void TurnOffFlipAnimationTrigger()
    {
        weapon.AnimationTurnOffFlip();
    }
    private void TurnOnFlipAnimationTrigger()
    {
        weapon.AnimationTurnOnFlip();
    }
    private void AnimationActionTrigger()
    {
        weapon.AnimationActionTrigger();
    }
}
