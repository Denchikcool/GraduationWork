using Denchik.ProjectileSystem.DataPackages;
using UnityEngine;

namespace Denchik.ProjectileSystem.Components
{
    public class ProjectileComponent : MonoBehaviour
    {
        protected Projectile projectile;

        protected Rigidbody2D Rigidbody2D
        {
            get
            {
                return projectile.Rigidbody2D;
            }
        }

        protected virtual void Awake()
        {
            projectile = GetComponent<Projectile>();

            projectile.OnInit += Init;
            projectile.OnReset += Reset;
            projectile.OnReceiveDataPackage += HandleReceiveDataPackage;
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {

        }

        protected virtual void FixedUpdate()
        {

        }

        protected virtual void Init()
        {

        }

        protected virtual void Reset()
        {

        }

        protected virtual void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {

        }

        protected virtual void OnDestroy()
        {
            projectile.OnInit -= Init;
            projectile.OnReset -= Reset;
            projectile.OnReceiveDataPackage -= HandleReceiveDataPackage;
        }
    }
}
