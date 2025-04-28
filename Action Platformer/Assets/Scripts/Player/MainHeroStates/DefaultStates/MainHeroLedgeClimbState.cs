using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroLedgeClimbState : MainHeroState
{
    private Vector2 _detectedPosition;
    private Vector2 _cornerPosition;
    private Vector2 _startPosition;
    private Vector2 _stopPosition;

    private bool _isHanging;
    private bool _isClimbing;
    private bool _jumpInput;
    private bool _isHeadTouchingWall;

    private int _xInput;
    private int _yInput;

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

        mainHero.SetVelocityZero();
        mainHero.transform.position = _detectedPosition;
        _cornerPosition = mainHero.DetermineCornerPosition();

        _startPosition.Set(_cornerPosition.x - (mainHero.FacingDirection * mainHeroData.StartOffset.x), _cornerPosition.y - mainHeroData.StartOffset.y);
        _stopPosition.Set(_cornerPosition.x + (mainHero.FacingDirection * mainHeroData.StopOffset.x), _cornerPosition.y + mainHeroData.StopOffset.y);

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

            mainHero.SetVelocityZero();
            mainHero.transform.position = _startPosition;

            if (_xInput == mainHero.FacingDirection && _isHanging && !_isClimbing)
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
        _isHeadTouchingWall = Physics2D.Raycast(_cornerPosition + (Vector2.up * 0.015f) + (Vector2.right * mainHero.FacingDirection * 0.015f), Vector2.up, mainHeroData.StandColliderHeight, mainHeroData.WhatIsGround);
        mainHero.Animator.SetBool("isHeadTouchingWall", _isHeadTouchingWall);
    }
}
