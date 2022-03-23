using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = ("New Melee Attack State Data"), menuName = ("Scriptable Data/State Data/Attack/Melee Attack Data"))]
public class MeleeAttackData : ScriptableObject
{
    [Min(0.2f)]
    public float attackRadius = 1.0f;
    [Range(0f, 100f)]
    public float attackDamage = 10f;
    public LayerMask whatIsTarget;
}
