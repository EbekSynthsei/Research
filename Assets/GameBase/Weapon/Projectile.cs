using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a projectile weapon.
/// </summary>
public class Projectile : MonoBehaviour
{
    private AttackData attackData;

    private float speed;
    private float travelDistance;
    private float xShootPoint;
    private Rigidbody2D Rb;

    [SerializeField]
    private float gravity;
    [SerializeField]
    private float damageRadius;
    [SerializeField]
    private LayerMask whatIsTarget;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private Transform damagePosition;
    private bool hasHitGround;

    private bool isGravityOn;

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();

        Rb.gravityScale = 0.0f;

        Rb.velocity = transform.right * speed;

        isGravityOn = false;

        xShootPoint = transform.position.x;
    }
    private void Update()
    {
        if (!hasHitGround)
        {
            attackData.position = transform.position;
            if (isGravityOn)
            {
                float angle = Mathf.Atan2(Rb.velocity.y, Rb.velocity.x)*Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
    private void FixedUpdate()
    {
        if(!hasHitGround)
        {
            attackData.position = transform.position;

            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsTarget);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);
            if (damageHit)
            {
                damageHit.transform.SendMessage("Damage", attackData);
                Destroy(gameObject);
            }
            if (groundHit)
            {
                hasHitGround = true;
                Rb.gravityScale = 0.0f;
                Rb.velocity = Vector2.zero;
            }

            if(Mathf.Abs(xShootPoint - transform.position.x)>= travelDistance && isGravityOn)
            {
                isGravityOn = true;
                Rb.gravityScale = gravity;
            }

        }
    }

    public void FireProjectile(float _speed, float _travelDistance, float _damageAmount)
    {
        speed = _speed;
        travelDistance = _travelDistance;
        attackData.damageAmount = _damageAmount;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
        Gizmos.DrawLine(damagePosition.position, damagePosition.position + (Vector3)Vector2.right * travelDistance);
    }
}
