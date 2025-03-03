using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for handling animation events and triggering corresponding methods in the Weapon class.
/// </summary>
public class AnimToWeapon : MonoBehaviour
{
    /// <summary>
    /// Reference to the Weapon component in the parent object.
    /// </summary>
    private Weapon weapon;

    /// <summary>
    /// Initializes the weapon reference.
    /// </summary>
    private void Start()
    {
        weapon = GetComponentInParent<Weapon>();
    }

    /// <summary>
    /// Triggers the AnimationFinishTrigger method in the Weapon class.
    /// </summary>
    private void AnimationFinishTrigger()
    {
        weapon.AnimationFinishTrigger();
    }

    /// <summary>
    /// Triggers the AnimationStartMovementTrigger method in the Weapon class.
    /// </summary>
    private void StartAnimationMovementTrigger()
    {
        weapon.AnimationStartMovementTrigger();
    }

    /// <summary>
    /// Triggers the AnimationStopMovementTrigger method in the Weapon class.
    /// </summary>
    private void StopAnimationMovementTrigger()
    {
        weapon.AnimationStopMovementTrigger();
    }

    /// <summary>
    /// Triggers the AnimationTurnOffFlip method in the Weapon class.
    /// </summary>
    private void TurnOffFlipAnimationTrigger()
    {
        weapon.AnimationTurnOffFlip();
    }

    /// <summary>
    /// Triggers the AnimationTurnOnFlip method in the Weapon class.
    /// </summary>
    private void TurnOnFlipAnimationTrigger()
    {
        weapon.AnimationTurnOnFlip();
    }

    /// <summary>
    /// Triggers the AnimationActionTrigger method in the Weapon class.
    /// </summary>
    private void AnimationActionTrigger()
    {
        weapon.AnimationActionTrigger();
    }
}
