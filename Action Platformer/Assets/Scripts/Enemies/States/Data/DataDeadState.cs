using UnityEngine;

[CreateAssetMenu(fileName = "newDeadStateData", menuName = "Data/State Data/Dead State")]
public class DataDeadState : ScriptableObject
{
    public GameObject DeathChunkParticle;
    public GameObject DeathBloodParticle;
}
