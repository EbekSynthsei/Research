using UnityEngine;
using LaniakeaCode.Utilities;

[CreateAssetMenu(fileName = ("New Touching Wall State Data"), menuName = ("LaniakeaTools/State Data/Touching Wall State Data"))]
public class TouchingWallData : ScriptableStateData
{
    [Range(0.0f, 100.0f)]
    public float wallSlideVelocity = 1f;
    [Range(0.0f, 100.0f)]
    public float wallClimbVelocity = 1f;

    [Range(0.0f, 100.0f)]
    public float wallJumpVelocity = 3f;
    [Range(0.0f, 1.0f)]
    public float wallJumpTime = 0.4f;

    public Vector2 wallJumpAngle = new Vector2(1, 2);
}
