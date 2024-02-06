using UnityEngine;
using LaniakeaCode.Utilities;

[CreateAssetMenu(fileName = ("New Search Target State Data"), menuName = ("LaniakeaTools/State Data/Search Target Data"))]
public class SearchTargetData : ScriptableStateData
{
    [Min(0)]
    public int amountOfTurns = 2;


    [Range(0.0f, 4.0f)]
    public float timeBetweenTurns = 0.5f;
}
