using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroAbilityState : MainHeroState
{
    protected bool isAbilityDone;

    private bool _isGrounded;

    private CollisionSenses _collisionSenses;
    private Movement _movement;

    protected Movement Movement
    {
        get => _movement ?? core.GetCoreComponent(ref _movement);
    }
    private CollisionSenses CollisionSenses
    {
        get => _collisionSenses ?? core.GetCoreComponent(ref _collisionSenses);
    }
    
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

        if (CollisionSenses)
        {
            _isGrounded = CollisionSenses.TouchingGround;
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (isAbilityDone)
        {
            if (_isGrounded && Movement?.CurrentVelocity.y < 0.01f)
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
