using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = ("New Idle State Data"), menuName = ("Scriptable Data/State Data/Idle Data"))]
public class IdleStateData : ScriptableObject
{
    [Min(0.0f)]
    public float minIdleTime = 2.0f;
    [Min(0.01f)]
    public float maxIdleTime = 4.0f;
}