using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = ("New In Air State Data"), menuName = ("LaniakeaTools/State Data/InAirData"))]
public class InAirStateData : ScriptableObject
{
    [Range(0.0f, 1.0f)]
    public float coyoteTime = 0.2f;
}
