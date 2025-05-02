using Denchik.Weapon;
using UnityEngine;
using Denchik.CoreSystem;

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
    public MainHeroCrouchIdleState MainHeroCrouchIdleState { get; private set; }
    public MainHeroCrouchMoveState MainHeroCrouchMoveState { get; private set; }
    public MainHeroAttackState PrimaryAttackState { get; private set; }
    public MainHeroAttackState SecondaryAttackState { get; private set; }
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
    public BoxCollider2D MovementCollider { get; private set; }
    public Core Core { get; private set; }
    #endregion

    #region Variables
    private Vector2 _workSpace;

    private Weapon _primaryWeapon;
    private Weapon _secondaryWeapon;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        _primaryWeapon = transform.Find("PrimaryWeapon").GetComponent<Weapon>();
        _secondaryWeapon = transform.Find("SecondaryWeapon").GetComponent<Weapon>();

        _primaryWeapon.SetCore(Core);
        _secondaryWeapon.SetCore(Core);

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
        MainHeroCrouchIdleState = new MainHeroCrouchIdleState(this, StateMachine, _mainHeroData, "crouchIdle");
        MainHeroCrouchMoveState = new MainHeroCrouchMoveState(this, StateMachine, _mainHeroData, "crouchMove");
        PrimaryAttackState = new MainHeroAttackState(this, StateMachine, _mainHeroData, "attack", _primaryWeapon);
        SecondaryAttackState = new MainHeroAttackState(this, StateMachine, _mainHeroData, "attack", _secondaryWeapon);
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        PlayerInputHandler = GetComponent<PlayerInputHandler>();
        Rigidbody = GetComponent<Rigidbody2D>();
        DashArrow = transform.Find("DashArrow");
        MovementCollider = GetComponent<BoxCollider2D>();

        StateMachine.Initialize(MainHeroIdleState);
    }

    private void Update()
    {
        Core.UpdateLogic();
        StateMachine.CurrentState.UpdateLogic();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.UpdatePhysics();
    }
    #endregion

    #region Other Functions

    public void SetColliderHeight(float height)
    {
        Vector2 center = MovementCollider.offset;
        _workSpace.Set(MovementCollider.size.x, height);
        center.y += (height - MovementCollider.size.y) / 2;
        MovementCollider.size = _workSpace;
        MovementCollider.offset = center;
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
