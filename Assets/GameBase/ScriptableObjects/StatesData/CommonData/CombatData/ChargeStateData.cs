using UnityEngine;
using LaniakeaCode.Utilities;

[CreateAssetMenu(fileName = ("New Charge State Data"), menuName = ("LaniakeaTools/State Data/Attack/Charge Data"))]
public class ChargeStateData : ScriptableStateData
{
    public float chargeSpeed = 2.0f;
    public float chargeTime = 2.0f;
}
