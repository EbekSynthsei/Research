using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = ("New Ranged Attack State Data"), menuName = ("LaniakeaTools/State Data/Attack/Ranged Attack Data"))]
public class RangedAttackData : ScriptableObject
{
    [Header("Ranged Attack Data")]
    public GameObject projectile;

    [Range(0.0f, 100.0f)]
    public float projectileDamage = 0.0f;
    [Range(0.0f, 100.0f)]
    public float projectileSpeed = 0.0f;
    [Range(0.0f, 100.0f)]
    public float projectileTravelDistance = 0.0f;
}

