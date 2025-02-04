using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dummy Container Class For All The Enums
/// </summary>
public class LEnums
{

}

/// <summary>
/// Represents the switch type.
/// </summary>
public enum SimplSwitchType
{
    /// <summary>
    /// Switch is on.
    /// </summary>
    On,
    /// <summary>
    /// Switch is off.
    /// </summary>
    Off
}

/// <summary>
/// Represents the language type.
/// </summary>
public enum LanguageType
{
    /// <summary>
    /// English language.
    /// </summary>
    English,
    /// <summary>
    /// Italian language.
    /// </summary>
    Italian
}

/// <summary>
/// Represents the end node type.
/// </summary>
public enum EndNodeType
{
    /// <summary>
    /// End the dialogue.
    /// </summary>
    End,
    /// <summary>
    /// Repeat the dialogue.
    /// </summary>
    Repeat,
    /// <summary>
    /// Go back in the dialogue.
    /// </summary>
    GoBack,
    /// <summary>
    /// Return to the start of the dialogue.
    /// </summary>
    ReturnToStart
}

/// <summary>
/// Represents the interaction type.
/// </summary>
public enum InteractionType
{
    /// <summary>
    /// Log interaction.
    /// </summary>
    LOG,
    /// <summary>
    /// No interaction.
    /// </summary>
    NONE,
    /// <summary>
    /// Interact.
    /// </summary>
    INTERACT,
    /// <summary>
    /// Damage interaction.
    /// </summary>
    DAMAGE
}

/// <summary>
/// Represents the damage type.
/// </summary>
public enum DamageType
{
    /// <summary>
    /// No damage.
    /// </summary>
    None,
    /// <summary>
    /// Base damage.
    /// </summary>
    Base
}

/// <summary>
/// Represents the entity type.
/// </summary>
public enum EntityType
{
    /// <summary>
    /// Base entity.
    /// </summary>
    Base,
    /// <summary>
    /// Player entity.
    /// </summary>
    Player,
    /// <summary>
    /// Enemy entity.
    /// </summary>
    Enemy,
    /// <summary>
    /// Non-player character entity.
    /// </summary>
    NPC,
    /// <summary>
    /// Barrel entity.
    /// </summary>
    Barrel,
    /// <summary>
    /// Ground entity.
    /// </summary>
    Ground,
    /// <summary>
    /// Projectile entity.
    /// </summary>
    Projectile
}

/// <summary>
/// Represents the global game state.
/// </summary>
public enum GlobalGameState
{
    /// <summary>
    /// Game is globally paused.
    /// </summary>
    GlobalPause,
    /// <summary>
    /// Game is globally continued.
    /// </summary>
    GlobalContinue,
    /// <summary>
    /// Game is in the main menu.
    /// </summary>
    GlobalMainMenu,
    /// <summary>
    /// Game is in the player menu.
    /// </summary>
    PlayerMenu,
    /// <summary>
    /// Player control state.
    /// </summary>
    PlayerControl,
    /// <summary>
    /// Player interaction state.
    /// </summary>
    PlayerInteraction,
    /// <summary>
    /// Passage state.
    /// </summary>
    Passage
}

/// <summary>
/// Represents the camera behaviour.
/// </summary>
public enum CameraBehaviour
{
    /// <summary>
    /// Camera follows the player.
    /// </summary>
    FollowPlayer,
    /// <summary>
    /// Camera follows a target.
    /// </summary>
    FollowTarget
}