using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : CoreComponent
{
    [SerializeField]
    private GameObject[] _deathParticles;

    private ParticleManager ParticleManager
    {
        get => _particleManager ? _particleManager : core.GetCoreComponent(ref _particleManager);
    }

    private Stats Stats
    {
        get => _stats ? _stats : core.GetCoreComponent(ref _stats);
    }

    private ParticleManager _particleManager;
    private Stats _stats;

    public void Die()
    {
        foreach (GameObject particle in _deathParticles)
        {
            ParticleManager.StartParticles(particle);
        }

        core.transform.parent.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Stats.OnHealthZero += Die;
    }

    private void OnDisable()
    {
        Stats.OnHealthZero -= Die;
    }
}
