using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataEntity : ScriptableObject
{
    public float WallCheckDistance;
    public float LedgeCheckDistance;

    public LayerMask WhatIsGround;
}
