using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using LaniakeaCode.Events;

namespace LaniakeaCode.Utilities
{
    [CreateAssetMenu(fileName ="BarrelType" , menuName = "LaniakeaScriptable/BarrelType")]
    public class BarrelTypeData : EntityData

    {
        [Space]
        [Header("BARREL")]
        [Space]
        [SerializeField]
        VoidEvent voidEvent;
        [SerializeField]
        List<InteractionType> interactionType;

    }
}
