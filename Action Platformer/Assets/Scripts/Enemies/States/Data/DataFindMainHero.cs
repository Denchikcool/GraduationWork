using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newFindMainHeroStateData", menuName = "Data/State Data/Find Main Hero State")]
public class DataFindMainHero : ScriptableObject
{
    public int CountOfTurns = 2;
    public float TimeBetweenTurns = 0.75f;
}
