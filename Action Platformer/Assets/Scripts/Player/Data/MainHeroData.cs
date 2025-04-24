using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMainHeroData", menuName = "Data/Player Data/Base Data")]
public class MainHeroData : ScriptableObject
{
    [Header("Move State")]
    public float MovementVelocity = 10.0f;
}
