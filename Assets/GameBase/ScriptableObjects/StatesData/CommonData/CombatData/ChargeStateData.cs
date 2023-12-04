using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = ("New Charge State Data"), menuName = ("LaniakeaTools/State Data/Attack/Charge Data"))]
public class ChargeStateData : ScriptableObject
{
    public float chargeSpeed = 2.0f;
    public float chargeTime = 2.0f;
}
