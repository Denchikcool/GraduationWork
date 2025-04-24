using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRangeAttackStateData", menuName = "Data/State Data/Range Attack State")]
public class DataRangeAttackState : ScriptableObject
{
    public GameObject RangeAttackItem;
    public float RangeAttackItemDamage = 10.0f;
    public float RangeAttackItemSpeed = 12.0f;
    public float RangeAttackItemTravelDistance = 10.0f;
}
