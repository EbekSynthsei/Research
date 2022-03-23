using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = ("New Target Detected State Data"), menuName = ("Scriptable Data/State Data/TargetDetectedData"))]
public class TargetDetectedStateData : ScriptableObject
{
    [Header("Long Range Action")]
    [Range(0.0f, 1f)]
    public float longRangeActionTime = 0.5f;
}
