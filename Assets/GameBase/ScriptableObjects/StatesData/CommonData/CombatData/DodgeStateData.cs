using UnityEngine;
using LaniakeaCode.Utilities;

[CreateAssetMenu(fileName = ("New Dodge State Data"), menuName = ("LaniakeaTools/State Data/Attack/Dodge Data"))]
public class DodgeStateData : ScriptableStateData
{

    [Range(0.0f,100.0f)]
    public float dodgeSpeed = 5f;
    [Min(0.0f)]
    public float dodgeTime = 0.01f;
    [Min(0.0f)]
    public float dodgeCooldown;
    public Vector2 dodgeAngle;

}
