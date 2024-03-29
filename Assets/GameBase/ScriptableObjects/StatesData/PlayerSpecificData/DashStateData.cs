using UnityEngine;
using LaniakeaCode.Utilities;
[CreateAssetMenu(fileName = ("New Dash State Data"), menuName = ("LaniakeaTools/State Data/Dash Data"))]
public class DashStateData : ScriptableStateData
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
