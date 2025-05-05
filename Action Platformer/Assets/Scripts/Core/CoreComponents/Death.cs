using UnityEngine;

namespace Denchik.CoreSystem
{
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
            Stats.Health.OnCurrentValueZero += Die;
        }

        private void OnDisable()
        {
            Stats.Health.OnCurrentValueZero -= Die;
        }
    }
}
