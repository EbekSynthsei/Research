using UnityEngine;
using LaniakeaCode.Utilities;

[CreateAssetMenu(fileName = ("New Target Detected State Data"), menuName = ("LaniakeaTools/State Data/TargetDetectedData"))]
public class TargetDetectedStateData : ScriptableStateData
{
    [Header("Long Range Action")]
    [Range(0.0f, 1f)]
    public float longRangeActionTime = 0.5f;
}
