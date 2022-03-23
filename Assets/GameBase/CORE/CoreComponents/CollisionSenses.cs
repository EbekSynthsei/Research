using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : COREComponent
{
    public BoxCollider2D HitBox { get => hitBox; set => hitBox = value; }
    public Transform WallCheck { get => wallCheck; private set => wallCheck = value; }
    public Transform LedgeCheck { get => ledgeCheck; private set => ledgeCheck = value; }
    public Transform TargetCheck { get => targetCheck; private set => targetCheck = value; }
    public Transform GroundCheck { get => groundCheck; private set => groundCheck = value; }
    public Transform CeilingCheck { get => ceilingCheck; private set => ceilingCheck = value; }
    public Transform LedgeVaultCheck { get => ledgeVaultCheck; private set => ledgeVaultCheck = value; }
    public float GroundCheckRadius { get => groundCheckRadius; private set => groundCheckRadius = value; }
    public float WallCheckDistance { get => wallCheckDistance; private set => wallCheckDistance = value; }
    public float LedgeCheckDistance { get => ledgeCheckDistance; private set => ledgeCheckDistance = value; }
    public LayerMask WhatIsGround { get => whatIsGround; private set => whatIsGround = value; }

    [Header("Main HitBox")] 
    [SerializeField]
    private BoxCollider2D hitBox;
    private Vector2 workVector;

    [Space]
    [Header("Environment Checks")]

    [SerializeField] private Transform wallCheck;

    [SerializeField] private Transform ledgeCheck;

    [SerializeField] private Transform targetCheck;

    [SerializeField] private Transform groundCheck;

    [SerializeField] private Transform ceilingCheck;

    [SerializeField] private Transform ledgeVaultCheck;

    [Space]
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float ledgeCheckDistance;


    [SerializeField] private LayerMask whatIsGround;

    [Space(2)]
    [Header("Target Checks")]

    [SerializeField] private float minAggroDistance;
    [SerializeField] private float maxAggroDistance;
    [SerializeField] private float closeRangeActionDistance;

    [SerializeField] private LayerMask whatIsTarget;

    #region Checks
    //ENVIRONMENT CHECKS
    public bool Wall
    { 
        get => Physics2D.Raycast(wallCheck.position, Vector2.right * Core.movement.FacingDirection, wallCheckDistance, whatIsGround);
    }
    public bool WallBack
    {
        get=> Physics2D.Raycast(ledgeCheck.position, Vector2.right * -Core.movement.FacingDirection, wallCheckDistance, whatIsGround);
    }
    public bool Ledge
    {
        get=> Physics2D.Raycast(ledgeCheck.position, Vector2.down, ledgeCheckDistance, whatIsGround);
    }

    public bool Ground
    {
        get=> Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }
    public bool Ceiling
    {
        get=> Physics2D.OverlapCircle(ceilingCheck.position, groundCheckRadius, whatIsGround);
    }

    public bool LedgeVault
    {
        get=> Physics2D.Raycast(ledgeVaultCheck.position, Vector2.right * Core.movement.FacingDirection, wallCheckDistance, whatIsGround);
    }

    public  bool BackLedgeVault
    {
        get=> Physics2D.Raycast(ledgeVaultCheck.position, Vector2.right * -Core.movement.FacingDirection, wallCheckDistance, whatIsGround);
    }
    #endregion

    #region Target Checks
    //TARGET CHECKS
    public bool TargetInMinAggroRange
    {
        get=> Physics2D.Raycast(targetCheck.position, Vector2.right, minAggroDistance, whatIsTarget);
    }
    public bool TargetInMaxAggroRange
    {
        get=> Physics2D.Raycast(targetCheck.position, Vector2.right, maxAggroDistance, whatIsTarget);
    }
    public bool TargetInCloseRangeAction
    {
        get=> Physics2D.Raycast(targetCheck.position, Vector2.right, closeRangeActionDistance, whatIsTarget);
    }
    #endregion
    public void SetColliderHeight(float height)
    {
        Vector2 center = HitBox.offset;
        workVector.Set(HitBox.size.x, height);
        center.y += (height - HitBox.size.y) / 2;
        HitBox.size = workVector;
        HitBox.offset = center;
    }
    public Vector2 GetCornerPosition()
    {
        float tolerance = 0.015f;
        RaycastHit2D xHit = Physics2D.Raycast(WallCheck.position, Vector2.right * Core.movement.FacingDirection, WallCheckDistance, WhatIsGround);

        float xDistance = xHit.distance;

        workVector.Set((xDistance + tolerance) * Core.movement.FacingDirection, 0f);

        RaycastHit2D yHit = Physics2D.Raycast(LedgeVaultCheck.position + (Vector3)workVector, Vector2.down, LedgeVaultCheck.position.y - WallCheck.position.y + tolerance, Core.collisionSenses.WhatIsGround);

        float yDistance = yHit.distance;

        workVector.Set(WallCheck.position.x + (xDistance * Core.movement.FacingDirection), LedgeCheck.position.y - yDistance);

        return workVector;
    }
#if UNITY_EDITOR

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
        Gizmos.DrawWireSphere(ceilingCheck.transform.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.transform.position, (Vector2)(wallCheck.transform.position + (Vector3)(Vector2.right*wallCheckDistance)));
        Gizmos.DrawLine(ledgeCheck.transform.position, (Vector2)(ledgeCheck.transform.position + (Vector3)Vector2.down*ledgeCheckDistance));
        Gizmos.DrawLine(ledgeVaultCheck.transform.position, (Vector2)(ledgeVaultCheck.transform.position + (Vector3)(Vector2.right * wallCheckDistance)));
    }
#endif
}