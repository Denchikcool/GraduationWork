using UnityEngine;

[System.Serializable]
public struct WeaponAttackDetails
{
    public string AttackName;
    public float MovementSpeed;
    public float DamageAmount;
    public float KnockbackStrength;
    public Vector2 KnockbackAngle;
}
