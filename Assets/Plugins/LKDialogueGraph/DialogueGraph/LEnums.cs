using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Dummy Container Class For All The Enums
    /// </summary>
    public class LEnums
    {

    }
    public enum SimplSwitchType
    {
        On,
        Off
    }

    public enum LanguageType
    {
        English,
        Italian
    }

    public enum EndNodeType
    {
        End,
        Repeat,
        GoBack,
        ReturnToStart
    }
    
    public enum InteractionType
{
    LOG,
    NONE,
    INTERACT,
    DAMAGE
}
    public enum DamageType
    {
        None,
        Base
    }

public enum EntityType
{
    Base,
    Player,
    Enemy,
    NPC,
    Barrel,
    Ground,
    Projectile
}