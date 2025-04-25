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
    #endregion

    #region Data
    [SerializeField]
    private MainHeroData _mainHeroData;
    #endregion

    #region Components
    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public PlayerInputHandler PlayerInputHandler { get; private set; }
    #endregion

    #region Transforms
    [SerializeField]
    private Transform _groundCheck;
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
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        PlayerInputHandler = GetComponent<PlayerInputHandler>();
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
    #endregion

    #region Other Functions
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
