using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Weapon Data", menuName ="Scriptable Data/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public int amountOfAttack { get; protected set; }
    public float[] movementSpeed { get; protected set; }

}
