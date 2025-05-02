using UnityEngine;

[CreateAssetMenu(fileName = "newChargeStateData", menuName = "Data/State Data/Charge State")]
public class DataChargeState : ScriptableObject
{
    public float ChargeSpeed = 6.0f;
    public float ChargeTime = 2.0f;
}
