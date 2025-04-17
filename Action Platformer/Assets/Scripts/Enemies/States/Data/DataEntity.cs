using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class DataEntity : ScriptableObject
{
    public float WallCheckDistance = 0.2f;
    public float LedgeCheckDistance = 0.4f;
    public float MaxAgroDistance = 4.0f;
    public float MinAgroDistance = 3.0f;

    public LayerMask WhatIsGround;
    public LayerMask WhatIsPlayer;
}
