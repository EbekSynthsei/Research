using UnityEngine;
using LaniakeaCode.Utilities;

[CreateAssetMenu(fileName =("New Move State Data"), menuName =("LaniakeaTools/State Data/Move Data"))]
public class MoveStateData : ScriptableStateData
{
    [Header("Entity Speed")]
    [Range(0.0f,100.0f)]
    public float moveSpeed = 3.0f;

    [Header("Crouch Modifier")]
    [Range(0.0f, 100.0f)]
    public float crouchSpeed = 2.0f;

    public float crouchColliderHeight = 0.2f;
    public float standColliderHeight = 0.4f;
    
}
