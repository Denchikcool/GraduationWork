using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHero : MonoBehaviour
{
    #region States
    public MainHeroStateMachine StateMachine { get; private set; }
    public MainHeroIdleState MainHeroIdleState { get; private set; }
    public MainHeroMoveState MainHeroMoveState { get; private set; }
    public MainHeroJumpState MainHeroJumpState { get; private set; }
    public MainHeroAirState MainHeroAirState { get; private set; }
    public MainHeroLandState MainHeroLandState { get; private set; }
    public MainHeroWallSlideState MainHeroWallSlideState { get; private set; }
    public MainHeroWallGrabState MainHeroWallGrabState { get; private set; }
    public MainHeroWallClimbState MainHeroWallClimbState { get; private set; }
    public MainHeroWallJumpState MainHeroWallJumpState { get; private set; }
    public MainHeroLedgeClimbState MainHeroLedgeClimbState { get; private set; }
    public MainHeroDashState MainHeroDashState { get; private set; }
    #endregion

    #region Data
    [SerializeField]
    private MainHeroData _mainHeroData;
    #endregion

    #region Components
    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public PlayerInputHandler PlayerInputHandler { get; private set; }
    public Transform DashArrow { get; private set; }
    #endregion

    #region Transforms
    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private Transform _wallCheck;
    [SerializeField]
    private Transform _ledgeCheck;
    #endregion

    #region Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    private Vector2 _workSpace;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        StateMachine = new MainHeroStateMachine();

        MainHeroIdleState = new MainHeroIdleState(this, StateMachine, _mainHeroData, "idle");
        MainHeroMoveState = new MainHeroMoveState(this, StateMachine, _mainHeroData, "move");
        MainHeroJumpState = new MainHeroJumpState(this, StateMachine, _mainHeroData, "inAir");
        MainHeroAirState = new MainHeroAirState(this, StateMachine, _mainHeroData, "inAir");
        MainHeroLandState = new MainHeroLandState(this, StateMachine, _mainHeroData, "land");
        MainHeroWallSlideState = new MainHeroWallSlideState(this, StateMachine, _mainHeroData, "wallSlide");
        MainHeroWallGrabState = new MainHeroWallGrabState(this, StateMachine, _mainHeroData, "wallGrab");
        MainHeroWallClimbState = new MainHeroWallClimbState(this, StateMachine, _mainHeroData, "wallClimb");
        MainHeroWallJumpState = new MainHeroWallJumpState(this, StateMachine, _mainHeroData, "inAir");
        MainHeroLedgeClimbState = new MainHeroLedgeClimbState(this, StateMachine, _mainHeroData, "ledgeClimbState");
        MainHeroDashState = new MainHeroDashState(this, StateMachine, _mainHeroData, "inAir");
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        PlayerInputHandler = GetComponent<PlayerInputHandler>();
        DashArrow = transform.Find("DashArrow");
        FacingDirection = 1;
        StateMachine.Initialize(MainHeroIdleState);
    }

    private void Update()
    {
        CurrentVelocity = Rigidbody.velocity;
        StateMachine.CurrentState.UpdateLogic();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.UpdatePhysics();
    }
    #endregion

    #region Set Functions
    public void SetHorizontalVelocity(float velocity)
    {
        _workSpace.Set(velocity, CurrentVelocity.y);
        Rigidbody.velocity = _workSpace;
        CurrentVelocity = _workSpace;
    }

    public void SetVerticalVelocity(float velocity)
    {
        _workSpace.Set(CurrentVelocity.x, velocity);
        Rigidbody.velocity = _workSpace;
        CurrentVelocity = _workSpace;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        _workSpace = direction * velocity;
        Rigidbody.velocity = _workSpace;
        CurrentVelocity = _workSpace;
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _workSpace.Set(angle.x * velocity * direction, angle.y * velocity);
        Rigidbody.velocity = _workSpace;
        CurrentVelocity = _workSpace;
    }

    public void SetVelocityZero()
    {
        Rigidbody.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }
    #endregion

    #region Check Functions
    public void CheckShouldFlip(int horizontalInput)
    {
        if(horizontalInput != 0 && horizontalInput != FacingDirection)
        {
            Flip();
        }
    }

    public bool CheckIfTouchingGround()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _mainHeroData.GroundCheckRadius, _mainHeroData.WhatIsGround);
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(_wallCheck.position, Vector2.right * FacingDirection, _mainHeroData.WallCheckDistance, _mainHeroData.WhatIsGround);
    }

    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(_wallCheck.position, Vector2.right * -FacingDirection, _mainHeroData.WallCheckDistance, _mainHeroData.WhatIsGround);
    }

    public bool CheckIfTouchingLedge()
    {
        return Physics2D.Raycast(_ledgeCheck.position, Vector2.right * FacingDirection, _mainHeroData.WallCheckDistance, _mainHeroData.WhatIsGround);
    }
    #endregion

    #region Other Functions

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(_wallCheck.position, Vector2.right * FacingDirection, _mainHeroData.WallCheckDistance, _mainHeroData.WhatIsGround);
        float xDistance = xHit.distance;
        _workSpace.Set(xDistance * FacingDirection, 0.0f);
        RaycastHit2D yHit = Physics2D.Raycast(_ledgeCheck.position + (Vector3)(_workSpace), Vector2.down, _ledgeCheck.position.y - _wallCheck.position.y, _mainHeroData.WhatIsGround);
        float yDistance = yHit.distance;

        _workSpace.Set(_wallCheck.position.x + (xDistance * FacingDirection), _ledgeCheck.position.y - yDistance);

        return _workSpace;
    }

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void AnimationTrigger()
    {
        StateMachine.CurrentState.AnimationTrigger();
    }

    private void AnimationFinishTrigger()
    {
        StateMachine.CurrentState.AnimationFinishTrigger();
    }
    #endregion
}
