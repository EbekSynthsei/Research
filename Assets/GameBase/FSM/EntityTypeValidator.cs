// EntityTypeValidator.cs
using UnityEngine;
using System;
using System.Collections.Generic;
using LaniakeaCode.Utilities;

/// <summary>
/// Enforces structural consistency (layer, tag, required components) based on EntityType.
/// Called from Entity.OnValidate() — no Inspector/editor asset involved, pure code enforcement.
/// </summary>
public static class EntityTypeValidator
{
    /// <summary>
    /// Describes the structural requirements associated with a given EntityType.
    /// </summary>
    private struct Requirements
    {
        public string ExpectedLayer;
        public string ExpectedTag;
        public Type[] RequiredComponents;
    }

    private static readonly Dictionary<EntityType, Requirements> Map = new Dictionary<EntityType, Requirements>
    {
        [EntityType.Player] = new Requirements
        {
            ExpectedLayer = "Player",
            ExpectedTag = "Player",
            RequiredComponents = new[] { typeof(CORE), typeof(AnimToFSM), typeof(AttackToFSM), typeof(Animator), typeof(Rigidbody2D) }
        },
        [EntityType.Enemy] = new Requirements
        {
            ExpectedLayer = "Enemy",
            ExpectedTag = "Enemy",
            RequiredComponents = new[] { typeof(CORE), typeof(AnimToFSM), typeof(AttackToFSM), typeof(Animator), typeof(Rigidbody2D) }
        },
        [EntityType.NPC] = new Requirements
        {
            ExpectedLayer = "NPC",
            ExpectedTag = "Untagged",
            RequiredComponents = new[] { typeof(CORE), typeof(AnimToFSM), typeof(Animator) }
        },
        [EntityType.Barrel] = new Requirements
        {
            ExpectedLayer = "Interactable",
            ExpectedTag = "Untagged",
            RequiredComponents = new[] { typeof(Animator) }
        },
        [EntityType.Projectile] = new Requirements
        {
            ExpectedLayer = "Projectile",
            ExpectedTag = "Untagged",
            RequiredComponents = new[] { typeof(Rigidbody2D) }
        },
        [EntityType.Ground] = new Requirements
        {
            ExpectedLayer = "Ground",
            ExpectedTag = "Untagged",
            RequiredComponents = Array.Empty<Type>()
        }
        // EntityType.Base intentionally omitted: generic fallback, no enforcement applied.
    };

    /// <summary>
    /// Validates the given GameObject against the structural rules defined for its EntityType.
    /// Emits Debug.LogWarning/LogError with a direct context reference; never throws or blocks the Editor.
    /// </summary>
    public static void Validate(GameObject go, EntityType entityType)
    {
        if (entityType == EntityType.Base) return;

        if (!Map.TryGetValue(entityType, out var req))
        {
            Debug.LogWarning($"[EntityTypeValidator] Nessuna regola di validazione definita per EntityType.{entityType} su '{go.name}'.", go);
            return;
        }

        ValidateLayer(go, req.ExpectedLayer);
        ValidateTag(go, req.ExpectedTag);
        ValidateComponents(go, req.RequiredComponents);
    }

    private static void ValidateLayer(GameObject go, string expectedLayer)
    {
        if (string.IsNullOrEmpty(expectedLayer)) return;

        int expectedLayerIndex = LayerMask.NameToLayer(expectedLayer);
        if (expectedLayerIndex == -1)
        {
            Debug.LogError($"[EntityTypeValidator] Layer '{expectedLayer}' non esiste nel progetto. Verifica Project Settings > Tags and Layers.", go);
            return;
        }

        if (go.layer != expectedLayerIndex)
        {
            Debug.LogWarning($"[EntityTypeValidator] '{go.name}': layer attuale '{LayerMask.LayerToName(go.layer)}' non corrisponde al layer atteso '{expectedLayer}' per il suo EntityType.", go);
        }
    }

    private static void ValidateTag(GameObject go, string expectedTag)
    {
        if (string.IsNullOrEmpty(expectedTag) || expectedTag == "Untagged") return;

        if (!go.CompareTag(expectedTag))
        {
            Debug.LogWarning($"[EntityTypeValidator] '{go.name}': tag attuale '{go.tag}' non corrisponde al tag atteso '{expectedTag}' per il suo EntityType.", go);
        }
    }

    private static void ValidateComponents(GameObject go, Type[] requiredComponents)
    {
        if (requiredComponents == null) return;

        foreach (var componentType in requiredComponents)
        {
            if (go.GetComponentInChildren(componentType) == null)
            {
                Debug.LogError($"[EntityTypeValidator] '{go.name}': componente richiesto '{componentType.Name}' assente (ricerca inclusi i figli) per il suo EntityType.", go);
            }
        }
    }
}