using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroAbilityState : MainHeroState
{
    protected bool isAbilityDone;

    private bool _isGrounded;
    public MainHeroAbilityState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void MakeChecks()
    {
        base.MakeChecks();

        _isGrounded = mainHero.CheckIfTouchingGround();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (isAbilityDone)
        {
            if (_isGrounded && mainHero.CurrentVelocity.y < 0.01f)
            {
                stateMachine.ChangeState(mainHero.MainHeroIdleState);
            }
            else
            {
                stateMachine.ChangeState(mainHero.MainHeroAirState);
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
