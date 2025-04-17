using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/State Data/Idle State")]
public class DataIdleState : ScriptableObject
{
    public float MinIdleTime = 1.0f;
    public float MaxIdleTime = 2.0f;
}
