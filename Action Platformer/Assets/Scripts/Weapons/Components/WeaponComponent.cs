using UnityEngine;
using Denchik.CoreSystem;

namespace Denchik.Weapon.Components
{
    public abstract class WeaponComponent : MonoBehaviour
    {
        protected Weapon weapon;

        //TODO
        //protected AnimationEventHandler EventHandler => weapon.EventHandler;
        protected AnimationEventHandler eventHandler;
        protected Core core => weapon.Core; 

        protected bool isAttackActive;

        protected virtual void Awake()
        {
            weapon = GetComponent<Weapon>();

            eventHandler = GetComponentInChildren<AnimationEventHandler>();
        }

        protected virtual void Start()
        {
            weapon.OnEnter += HandleEnter;
            weapon.OnExit += HandleExit;
        }

        public virtual void Init()
        {

        }

        protected virtual void HandleEnter()
        {
            isAttackActive = true;
        }

        protected virtual void HandleExit()
        {
            isAttackActive = false;
        }

        protected virtual void OnDestroy()
        {
            weapon.OnEnter -= HandleEnter;
            weapon.OnExit -= HandleExit;
        }
    }

    public abstract class WeaponComponent<firstType, secondType> : WeaponComponent where firstType : ComponentData<secondType> where secondType : AttackData
    {
        protected firstType data;
        protected secondType currentAttackData;

        public override void Init()
        {
            base.Init();

            data = weapon.WeaponData.GetData<firstType>();
        }

        protected override void HandleEnter()
        {
            base.HandleEnter();

            currentAttackData = data.AttackData[weapon.CurrentAttackCounter];
        }
    }
}
