using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LaniakeaCode.Utilities;

public class Barrel : Entity
{
    [Range(0.1f, 2.0f)]
    public float size = 1;
    public string barrelname = "New Map Object";
    public Color gizmoColor = Color.red;

    private BarrelState barrelState;

    [SerializeField]
    public BarrelTypeData typeData;
    [SerializeField]
    private BarrelStateData initState;

    public void ApplyColor()
    {
        Debug.Log("Update All Barrel From Manager");
    }

    public override void Start()
    {
        base.Start();

        //Move
        barrelState = new BarrelState(this, stateMachine, "Idle", initState, this);

        stateMachine.Init(barrelState);
    }
    void OnEnable()
    {
        ApplyColor();
        BarrelManager.barrels.Add(this);
    }
    void OnDisable() => BarrelManager.barrels.Remove(this);

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, size);
    }
}
