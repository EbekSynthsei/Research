using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Debug.Log("Missing CoreComponent");
        }
    }

    public void LogicUpdate()
    {
        movement.LogicUpdate();
    }
}
