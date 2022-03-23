using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Aggressive Weapon Data", menuName = "Scriptable Data/Weapon Data/Aggressive Weapon")]
public class AggroWeaponData : WeaponData
{
    [SerializeField] private WeaponAttackData[] attackDetails;
    public WeaponAttackData[] AttackDetails { get => attackDetails; set => attackDetails = value; }
    private void OnEnable()
    {
        amountOfAttack = attackDetails.Length;
        movementSpeed = new float[amountOfAttack];
        for (int i = 0; i < amountOfAttack; i++)
        {
            movementSpeed[i] = attackDetails[i].movementSpeed;
        }

    }
}
