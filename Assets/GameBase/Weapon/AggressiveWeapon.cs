using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveWeapon : Weapon
{
    private List<IDamageable> detectedDamageable = new List<IDamageable>();
    protected AggroWeaponData aggressiveWeaponData;
    protected override void Awake()
    {
        base.Awake();
        if(weaponData.GetType() == typeof(AggroWeaponData))
        {
            aggressiveWeaponData = (AggroWeaponData)weaponData;
        }
        else
        {
            Debug.LogError("Wrong Weapon Data", this);
        }
    }
    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
        CheckMeleeAttack();
    }
    private void CheckMeleeAttack()
    {
        WeaponAttackData details = aggressiveWeaponData.AttackDetails[AttackCounter];
        foreach(IDamageable item in detectedDamageable)
        {
            item.Damage(details.damageAmount);
        }
    }
    public void AddToDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            detectedDamageable.Add(damageable);
        }
    }
    public void RemoveFromDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            detectedDamageable.Remove(damageable);
        }
    }
}
