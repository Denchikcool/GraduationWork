using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroAttackState : MainHeroAbilityState
{
    private Weapon _weapon;

    private float _velocityToSet;

    private int _xInput;

    private bool _setVelocity;
    private bool _shouldCheckFlip;

    public MainHeroAttackState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _setVelocity = false;
        _weapon.EnterWeapon();
    }

    public override void Exit()
    {
        base.Exit();

        _weapon.ExitWeapon();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        _xInput = mainHero.PlayerInputHandler.NormalizeInputX;

        if (_shouldCheckFlip)
        {
            Movement?.CheckShouldFlip(_xInput);
        }
        
        if(_setVelocity)
        {
            Movement?.SetHorizontalVelocity(_velocityToSet * Movement.FacingDirection);
        }
    }

    public void SetWeapon(Weapon weapon)
    {
        this._weapon = weapon;
        _weapon.InitializeWeapon(this, core);
    }

    public void SetMainHeroVelocity(float velocity)
    {
        Movement?.SetHorizontalVelocity(velocity * Movement.FacingDirection);
        _velocityToSet = velocity;
        _setVelocity = true;
    }

    public void SetFlipCheck(bool flipCheck)
    {
        _shouldCheckFlip = flipCheck;
    }

    #region Animations Triggers
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        isAbilityDone = true;
    }
    #endregion
}
