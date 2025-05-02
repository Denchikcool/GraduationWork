using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class DataEntity : ScriptableObject
{
    public float WallCheckDistance = 0.2f;
    public float LedgeCheckDistance = 0.4f;
    public float MaxAgroDistance = 4.0f;
    public float MinAgroDistance = 3.0f;
    public float CloseRangeActionDistance = 1.0f;
    public float MaxHealth = 30.0f;
    public float DamageHopSpeed = 3.0f;
    public float GroundCheckRadius = 0.3f;
    public float StunResistance = 3.0f;
    public float StunRecoveryTime = 2.0f;

    public LayerMask WhatIsGround;
    public LayerMask WhatIsPlayer;

    public GameObject HitParticle;
}
