using UnityEngine;
using LaniakeaCode.Utilities;

[CreateAssetMenu(fileName = ("New In Air State Data"), menuName = ("LaniakeaTools/State Data/InAirData"))]
public class InAirStateData : ScriptableStateData
{
    [Range(0.0f, 1.0f)]
    public float coyoteTime = 0.2f;
}
