using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = ("New Dash State Data"), menuName = ("Scriptable Data/State Data/Dash Data"))]
public class DashStateData : ScriptableObject
{
    [Range(0.0f, 10.0f)]
    public float dashCooldown = 0.5f;
    [Range(0.0f, 10.0f)]
    public float dashTime = 30f;
    public float maxHoldTime = 1f;
    public float holdTimeScale = 0.2f;
    public float dashVelocity = 6.0f;
    public float drag = 10f;
    public float distanceForAfterImage = 0.5f;
}
