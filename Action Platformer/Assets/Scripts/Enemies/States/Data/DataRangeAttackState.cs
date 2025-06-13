using UnityEngine;

[CreateAssetMenu(fileName = "newRangeAttackStateData", menuName = "Data/State Data/Range Attack State")]
public class DataRangeAttackState : ScriptableObject
{
    public GameObject RangeAttackItem;
    public float RangeAttackItemDamage = 10.0f;
    public float RangeAttackItemSpeed = 12.0f;
    public float RangeAttackItemTravelDistance = 10.0f;
    public float DamageRadius = 0.5f;
    public float Gravity = 1f;
    public LayerMask WhatIsGround;
    public LayerMask WhatIsPlayer;
    public Vector2 KnockbackAngle;
    public float KnockbackStrength = 5.0f;
}
