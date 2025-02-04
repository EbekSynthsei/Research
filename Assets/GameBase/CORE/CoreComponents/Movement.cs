using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LaniakeaCode.Utilities;

/// <summary>
/// Handles movement logic for the core.
/// </summary>
public class Movement : COREComponent
{
    public Rigidbody2D Rb { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    [HideInInspector]
    public Vector2 holdPosition;
    public Vector2 startOffset;
    public Vector2 stopOffset;
    private Vector2 workVector;

    [SerializeField] [ExposedScriptableObject] public MoveStateData moveStateData;

    [SerializeField] [ExposedScriptableObject] public JumpStateData jumpStateData;
                     
    [SerializeField] [ExposedScriptableObject] public InAirStateData inAirStateData;
                     
    [SerializeField] [ExposedScriptableObject] public LandStateData landStateData;
                     
    [SerializeField] [ExposedScriptableObject] public TouchingWallData touchingWallData;
                     
    [SerializeField] [ExposedScriptableObject] public DashStateData dashStateData;
    protected override void Awake()
    {
        base.Awake();
        Rb = GetComponentInParent<Rigidbody2D>();
        FacingDirection = 1;
    }

    public void LogicUpdate()
    {
        CurrentVelocity = Rb.velocity;
    }
    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workVector.Set(angle.x * velocity * direction, angle.y * velocity);
        Rb.velocity = workVector;
        CurrentVelocity = workVector;
    }
    public virtual void SetVelocity(float velocity, Vector2 direction)
    {
        workVector = direction * velocity;
        Rb.velocity = workVector;
        CurrentVelocity = workVector;
    }
    public virtual void SetVelocityX(float velocity)
    {
        workVector.Set(velocity, CurrentVelocity.y);
        Rb.velocity = workVector;
        CurrentVelocity = workVector;
    }
    public virtual void SetVelocityY(float velocity)
    {
        workVector.Set(CurrentVelocity.x, velocity);
        Rb.velocity = workVector;
        CurrentVelocity = workVector;
    }
    public virtual void SetVelocityFacingDirection(float velocity)
    {
        workVector.Set(FacingDirection * velocity, CurrentVelocity.y);
        Rb.velocity = workVector;
        CurrentVelocity = workVector;
    }
    public virtual void SetVelocityZero()
    {
        Rb.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }
    public virtual void Flip()
    {
        FacingDirection *= -1;
        Rb.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    public virtual void CheckIfShouldFlip(int xinput)
    {
        if (xinput != 0 && xinput != FacingDirection)
        {
            Flip();
        }
    }
    public virtual void HoldPosition()
    {
        Rb.transform.position = holdPosition;

        Core.movement.SetVelocityZero();
    }
}
