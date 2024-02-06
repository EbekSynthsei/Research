using UnityEngine;
using LaniakeaCode.Utilities;

[CreateAssetMenu(fileName = ("New Idle State Data"), menuName = ("LaniakeaTools/State Data/Idle Data"))]
public class IdleStateData : ScriptableStateData
{
    [Min(0.0f)]
    public float minIdleTime = 2.0f;
    [Min(0.01f)]
    public float maxIdleTime = 4.0f;
}