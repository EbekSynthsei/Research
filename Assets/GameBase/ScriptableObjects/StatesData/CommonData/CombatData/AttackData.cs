using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AttackData
{
    public Vector2 position;
    public float damageAmount;
    public float stunDamageAmount;
}

[System.Serializable]
public struct WeaponAttackData
{
    public string attackName;
    public float movementSpeed;
    public float damageAmount;
}