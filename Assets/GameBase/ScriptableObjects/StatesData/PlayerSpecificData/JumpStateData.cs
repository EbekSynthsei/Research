using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = ("New Jump State Data"), menuName = ("LaniakeaTools/State Data/Jump Data"))]
public class JumpStateData : ScriptableObject
{
    [Header("Jump Velocity")]
    [Range(0.0f, 100.0f)]
    public float jumpVelocity = 2.0f;

    public int amountOfJumps = 2;
}
