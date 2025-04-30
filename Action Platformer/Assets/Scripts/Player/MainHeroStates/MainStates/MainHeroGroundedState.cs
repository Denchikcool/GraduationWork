using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroGroundedState : MainHeroState
{
    protected int inputXPosition;
    protected int inputYPosition;

    protected bool isHeadTouchingWall;

    private bool _jumpInput;
    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _grabInput;
    private bool _isTouchingLedge;
    private bool _dashInput;

    protected Movement Movement
    {
        get => _movement ?? core.GetCoreComponent(ref _movement);

    }
    private CollisionSenses CollisionSenses
    {
        get => _collisionSenses ?? core.GetCoreComponent(ref _collisionSenses);
    }

    private CollisionSenses _collisionSenses;
    private Movement _movement;

    public MainHeroGroundedState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        mainHero.MainHeroJumpState.ResetAmountOfJumpsLeft();
        mainHero.MainHeroDashState.ResetCanDash();
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
            _isTouchingWall = CollisionSenses.TouchingWall;
            _isTouchingLedge = CollisionSenses.LedgeHorizontal;
            isHeadTouchingWall = CollisionSenses.HeadTouchingWall;
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        inputXPosition = mainHero.PlayerInputHandler.NormalizeInputX;
        inputYPosition = mainHero.PlayerInputHandler.NormalizeInputY;
        _jumpInput = mainHero.PlayerInputHandler.JumpInput;
        _grabInput = mainHero.PlayerInputHandler.GrabInput;
        _dashInput = mainHero.PlayerInputHandler.DashInput;

        if (mainHero.PlayerInputHandler.AttackInput[(int)CombatInput.primary] && !isHeadTouchingWall)
        {
            stateMachine.ChangeState(mainHero.PrimaryAttackState);
        }
        else if (mainHero.PlayerInputHandler.AttackInput[(int)CombatInput.secondary] && !isHeadTouchingWall)
        {
            stateMachine.ChangeState(mainHero.SecondaryAttackState);
        }
        else if (_jumpInput && mainHero.MainHeroJumpState.CanJump())
        {
            stateMachine.ChangeState(mainHero.MainHeroJumpState);
        }
        else if (!_isGrounded)
        {
            mainHero.MainHeroAirState.StartCoyoteTime();
            stateMachine.ChangeState(mainHero.MainHeroAirState);
        }
        else if(_isTouchingWall && _grabInput && _isTouchingLedge)
        {
            stateMachine.ChangeState(mainHero.MainHeroWallGrabState);
        }
        else if (_dashInput && mainHero.MainHeroDashState.CheckIfCanDash() && !isHeadTouchingWall)
        {
            stateMachine.ChangeState(mainHero.MainHeroDashState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
