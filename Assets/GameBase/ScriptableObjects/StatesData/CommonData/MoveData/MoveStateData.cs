using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =("New Move State Data"), menuName =("Scriptable Data/State Data/Move Data"))]
public class MoveStateData : ScriptableObject
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
