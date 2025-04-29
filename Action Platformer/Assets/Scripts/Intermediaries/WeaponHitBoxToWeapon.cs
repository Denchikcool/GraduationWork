using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitBoxToWeapon : MonoBehaviour
{
    private AggressiveWeapon _aggressiveWeapon;

    private void Awake()
    {
        _aggressiveWeapon = GetComponentInParent<AggressiveWeapon>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        _aggressiveWeapon.AddToDetected(collider);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        _aggressiveWeapon.RemoveFromDetected(collider);
    }
}
