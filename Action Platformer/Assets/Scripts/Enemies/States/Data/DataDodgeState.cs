using UnityEngine;

[CreateAssetMenu(fileName = "newDodgeStateData", menuName = "Data/State Data/Dodge State")]
public class DataDodgeState : ScriptableObject
{
    public float DodgeSpeed = 10.0f;
    public float DodgeTime = 0.2f;
    public float DodgeCooldown = 2.0f;
    public Vector2 DodgeAngle;
}
