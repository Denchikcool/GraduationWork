using UnityEngine;

namespace Denchik.Weapon.Components
{
    public class ActionHitBoxData : ComponentData<AttackActionHitBox>
    {
        [field: SerializeField]
        public LayerMask DetectableLayers { get; private set; }

        public ActionHitBoxData()
        {
            ComponentDependency = typeof(ActionHitBox);
        }
    }
}
