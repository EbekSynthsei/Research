using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Animator BaseAnimator;
    protected Animator WeaponAnimator;

    protected O_AttackState attackState;

    [SerializeField] protected WeaponData weaponData;

    protected int AttackCounter;
    protected virtual void Awake()
    {
        if(transform.Find("Base") == null || transform.Find("Weapon") == null)
        {
            Debug.LogError("Missing Base or Weapon in " + this.name);
        }

        BaseAnimator = transform.Find("Base").GetComponent<Animator>();
        WeaponAnimator = transform.Find("Weapon").GetComponent<Animator>();

        gameObject.SetActive(false);
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        if (AttackCounter >= weaponData.amountOfAttack)
        {
            AttackCounter = 0;
        }
        BaseAnimator.SetBool("Attack", true);
        WeaponAnimator.SetBool("Attack", true);

        BaseAnimator.SetInteger("AttackCounter", AttackCounter);
        WeaponAnimator.SetInteger("AttackCounter", AttackCounter);
    }

    public virtual void ExitWeapon()
    {
        BaseAnimator.SetBool("Attack", false);
        WeaponAnimator.SetBool("Attack", false);

        AttackCounter++;

        gameObject.SetActive(false);
    }

    #region AnimationTriggers

    public virtual void AnimationFinishTrigger()
    {
        attackState.AnimationFinishTrigger();
    }
    public virtual void AnimationStartMovementTrigger()
    {
        attackState.SetPlayerVelocity(weaponData.movementSpeed[AttackCounter]);
    }
    public virtual void AnimationStopMovementTrigger()
    {
        attackState.SetPlayerVelocity(0f);
    }
    public virtual void AnimationTurnOffFlip()
    {
        attackState.SetFlipCheck(false);
    }
    public virtual void AnimationTurnOnFlip()
    {
        attackState.SetFlipCheck(true);
    }

    public virtual void AnimationActionTrigger()
    {

    }
    #endregion

    public void InitializeWeapon(O_AttackState attackState)
    {
        this.attackState = attackState;
    }
}
