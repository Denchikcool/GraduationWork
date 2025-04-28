using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroAttackState : MainHeroAbilityState
{
    private Weapon _weapon;
    public MainHeroAttackState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _weapon.EnterWeapon();
    }

    public override void Exit()
    {
        base.Exit();

        _weapon.ExitWeapon();
    }

    public void SetWeapon(Weapon weapon)
    {
        this._weapon = weapon;
        _weapon.InitializeWeapon(this);
    }

    #region Animations Triggers
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        isAbilityDone = true;
    }
    #endregion
}
