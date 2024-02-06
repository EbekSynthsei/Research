using UnityEngine;
using LaniakeaCode.Utilities;

[CreateAssetMenu(fileName ="New Weapon Data", menuName ="LaniakeaTools/Weapon Data")]
public class WeaponData : ScriptableTypeData
{
    public int amountOfAttack { get; protected set; }
    public float[] movementSpeed { get; protected set; }

}
