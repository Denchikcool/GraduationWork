using UnityEngine;
using Denchik.CoreSystem;

public class MainHeroLedgeClimbState : MainHeroState
{
    private Vector2 _detectedPosition;
    private Vector2 _cornerPosition;
    private Vector2 _startPosition;
    private Vector2 _stopPosition;
    private Vector2 _workSpace;

    private bool _isHanging;
    private bool _isClimbing;
    private bool _jumpInput;
    private bool _isHeadTouchingWall;

    private int _xInput;
    private int _yInput;

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

    public MainHeroLedgeClimbState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        mainHero.Animator.SetBool("climbLedge", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        _isHanging = true;
    }

    public override void Enter()
    {
        base.Enter();

        Movement?.SetVelocityZero();
        mainHero.transform.position = _detectedPosition;
        _cornerPosition = DetermineCornerPosition();

        _startPosition.Set(_cornerPosition.x - (Movement.FacingDirection * mainHeroData.StartOffset.x), _cornerPosition.y - mainHeroData.StartOffset.y);
        _stopPosition.Set(_cornerPosition.x + (Movement.FacingDirection * mainHeroData.StopOffset.x), _cornerPosition.y + mainHeroData.StopOffset.y);

        mainHero.transform.position = _startPosition;
    }

    public override void Exit()
    {
        base.Exit();

        _isHanging = false;

        if (_isClimbing)
        {
            mainHero.transform.position = _stopPosition;
            _isClimbing = false;
        }
    }

    public void SetDetectedPosition(Vector2 position)
    {
        _detectedPosition = position;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (isAnimationFinish)
        {
            if (_isHeadTouchingWall)
            {
                stateMachine.ChangeState(mainHero.MainHeroCrouchIdleState);
            }
            else
            {
                stateMachine.ChangeState(mainHero.MainHeroIdleState);
            }
        }
        else
        {
            _xInput = mainHero.PlayerInputHandler.NormalizeInputX;
            _yInput = mainHero.PlayerInputHandler.NormalizeInputY;
            _jumpInput = mainHero.PlayerInputHandler.JumpInput;

            Movement?.SetVelocityZero();
            mainHero.transform.position = _startPosition;

            if (_xInput == Movement.FacingDirection && _isHanging && !_isClimbing)
            {
                CheckSpace();
                _isClimbing = true;
                mainHero.Animator.SetBool("climbLedge", true);
            }
            else if (_yInput == -1 && _isHanging && !_isClimbing)
            {
                stateMachine.ChangeState(mainHero.MainHeroAirState);
            }
            else if (_jumpInput && !_isClimbing)
            {
                mainHero.MainHeroWallJumpState.DetermineWallJumpDirection(true);
                stateMachine.ChangeState(mainHero.MainHeroWallJumpState);
            }
        }
    }

    private void CheckSpace()
    {
        _isHeadTouchingWall = Physics2D.Raycast(_cornerPosition + (Vector2.up * 0.015f) + (Vector2.right * Movement.FacingDirection * 0.015f), Vector2.up, mainHeroData.StandColliderHeight, CollisionSenses.WhatIsGround);
        mainHero.Animator.SetBool("isHeadTouchingWall", _isHeadTouchingWall);
    }

    private Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(CollisionSenses.WallCheck.position, Vector2.right * Movement.FacingDirection, CollisionSenses.WallCheckDistance, CollisionSenses.WhatIsGround);
        float xDistance = xHit.distance;
        _workSpace.Set((xDistance + 0.015f) * Movement.FacingDirection, 0.0f);
        RaycastHit2D yHit = Physics2D.Raycast(CollisionSenses.LedgeCheckHorizontal.position + (Vector3)(_workSpace), Vector2.down, CollisionSenses.LedgeCheckHorizontal.position.y - CollisionSenses.WallCheck.position.y + 0.015f, CollisionSenses.WhatIsGround);
        float yDistance = yHit.distance;

        _workSpace.Set(CollisionSenses.WallCheck.position.x + (xDistance * Movement.FacingDirection), CollisionSenses.LedgeCheckHorizontal.position.y - yDistance);

        return _workSpace;
    }
}
