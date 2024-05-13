using System.Collections.Generic;
using UnityEngine;
using LaniakeaCode.Utilities;

[CreateAssetMenu(fileName = ("New Dead State Data"), menuName = ("LaniakeaTools/State Data/Dead Data"))]
public class DeadStateData : ScriptableStateData
{
    
    public GameObject DeadObject;
    
    [Header("OnDeath Events")]
    public List<ScriptableEvent> scriptableEvent;
}
