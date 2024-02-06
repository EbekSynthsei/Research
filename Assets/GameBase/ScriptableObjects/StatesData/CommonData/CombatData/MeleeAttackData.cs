using UnityEngine;
using LaniakeaCode.Utilities;

[CreateAssetMenu(fileName = ("New Melee Attack State Data"), menuName = ("LaniakeaTools/State Data/Attack/Melee Attack Data"))]
public class MeleeAttackData : ScriptableStateData
{
    [Min(0.2f)]
    public float attackRadius = 1.0f;
    [Range(0f, 100f)]
    public float attackDamage = 10f;
    public LayerMask whatIsTarget;
}
