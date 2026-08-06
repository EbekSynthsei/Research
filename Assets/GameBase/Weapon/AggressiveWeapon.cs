using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveWeapon : Weapon
{
    private readonly List<IDamageable> detectedDamageable = new List<IDamageable>();
    protected AggroWeaponData aggressiveWeaponData;
    private bool isDataValid;

    protected override void Awake()
    {
        base.Awake();
        if (weaponData.GetType() == typeof(AggroWeaponData))
        {
            aggressiveWeaponData = (AggroWeaponData)weaponData;
            isDataValid = true;
        }
        else
        {
            Debug.LogError("Wrong Weapon Data", this);
            isDataValid = false;
        }
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
        CheckMeleeAttack();
    }

    private void CheckMeleeAttack()
    {
        if (!isDataValid)
        {
            return;
        }

        if (AttackCounter < 0 || AttackCounter >= aggressiveWeaponData.AttackDetails.Length)
        {
            Debug.LogError($"{name}: AttackCounter ({AttackCounter}) out of range for AttackDetails " +
                $"(length {aggressiveWeaponData.AttackDetails.Length}). Controlla amountOfAttack in WeaponData.", this);
            return;
        }

        WeaponAttackData details = aggressiveWeaponData.AttackDetails[AttackCounter];

        for (int i = detectedDamageable.Count - 1; i >= 0; i--)
        {
            detectedDamageable[i]?.Damage(details.damageAmount);
        }
    }

    public void AddToDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null && !detectedDamageable.Contains(damageable))
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