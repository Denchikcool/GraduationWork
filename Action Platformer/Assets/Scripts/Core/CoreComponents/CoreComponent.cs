using Denchik.Interfaces;
using UnityEngine;

namespace Denchik.CoreSystem
{
    public class CoreComponent : MonoBehaviour, ILogicUpdate
    {
        protected Core core;

        protected virtual void Awake()
        {
            core = transform.parent.GetComponent<Core>();

            if (core == null)
            {
                Debug.LogError("No core on the parent!");
            }

            core.AddComponent(this);
        }

        public virtual void UpdateLogic()
        {

        }
    }
}
