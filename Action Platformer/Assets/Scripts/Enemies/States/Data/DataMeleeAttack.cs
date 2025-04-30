using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State")]
public class DataMeleeAttack : ScriptableObject
{
    public float AttackRadius = 0.5f;
    public float AttackDamage = 10.0f;
    public float KnockbackStrength = 10.0f;

    public LayerMask WhatIsPlayer;

    public Vector2 KnockbackAngle = Vector2.one;
}
