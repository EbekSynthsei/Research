using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = ("New Entity Data"), menuName = ("Scriptable Data/Entity Data/Entity Data"))]
public class EntityData : ScriptableObject
{
    [Header("Stats")]

    [Range(0.0f, 100.0f)]

    public float maxHealth = 20.0f;

    [Header("Checks")]
    [Space]

    [Header("Ground")]
    [Range(0.01f, 100f)]
    public float wallCheckDistance = 2.0f;
    [Range(0.01f, 100f)]
    public float ledgeCheckDistance = 1.8f;
    [Range(0.01f, 100f)]
    public float groundCheckRadius = 0.2f;

    [Header("Ledge Climb")]
    public Vector2 startOffset;
    public Vector2 stopOffset;

    public LayerMask whatIsGround;

    [Header("Target")]

    [Range(0.1f, 100f)]
    public float minAggroDistance = 2.0f;

    [Range(0.1f, 100f)]
    public float maxAggroDistance = 4.0f;

    public LayerMask whatIsTarget;

    [Header("Attack Data")]
    public float closeRangeActionDistance = 1.0f;

    public float damageHopSpeed = 3.0f;

    [Header("Stun")]
    public float stunResistance = 3.0f;
    public float stunRecoveryTime = 2.0f;

    [Header("FX")]
    public GameObject damageParticle;
}
