using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object Core that serves as a building block
/// </summary>
public class CORE : MonoBehaviour
{
    public Movement movement { get; private set; }
    public CollisionSenses collisionSenses { get; private set; }
    public CombatSenses combatSenses { get; private set;}

    private void Awake()
    {
        movement = GetComponentInChildren<Movement>();
        collisionSenses = GetComponentInChildren<CollisionSenses>();
        combatSenses = GetComponentInChildren<CombatSenses>();
        if (!movement || !collisionSenses || !combatSenses)
        {
            Debug.LogError("Missing CoreComponent", this);
        }
    }

    public void LogicUpdate()
    {
        movement.LogicUpdate();
    }
}
