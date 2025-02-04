using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Links hitbox detection to weapon functionality.
/// </summary>
public class HitBoxToWeapon : MonoBehaviour
{
    private AggressiveWeapon weapon;

    private void Awake()
    {
        weapon = GetComponentInParent<AggressiveWeapon>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        weapon.AddToDetected(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        weapon.RemoveFromDetected(collision);
    }
}
