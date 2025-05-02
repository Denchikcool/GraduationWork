using UnityEngine;

[CreateAssetMenu(fileName = "newStunStateData", menuName = "Data/State Data/Stun State")]
public class DataStunState : ScriptableObject
{
    public float StunTime = 3.0f;
    public float StunKnockbackTime = 0.2f;
    public float StunKnockbackSpeed = 20.0f;
    public Vector2 StunKnockbackAngle;
}
