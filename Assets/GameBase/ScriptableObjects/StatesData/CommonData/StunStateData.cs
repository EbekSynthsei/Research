using UnityEngine;
using LaniakeaCode.Utilities;
[CreateAssetMenu(fileName = ("New Stun State Data"), menuName = ("LaniakeaTools/State Data/Attack/StunData"))]
public class StunStateData : ScriptableStateData
{
    [Header("Stun")]
    [Range(0.2f, 10.0f)]
    public float stunTime = 3.0f;


    [Header("KnockBack")]

    [Min(0.0f)]
    public float stunKnockBackTime;

    [Range(0.1f, 10f)]
    public float stunKnockBackSpeed = 4f;
    public Vector2 stunAngleKnockBack;
}
