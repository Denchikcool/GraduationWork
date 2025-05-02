using Denchik.CoreSystem;

public class MainHeroTouchWallState : MainHeroState
{
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool grabInput;
    protected bool jumpInput;
    protected bool isTouchingLedge;

    protected int xInput;
    protected int yInput;

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

    public MainHeroTouchWallState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void Enter()
    {
        base.Enter();
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
            isGrounded = CollisionSenses.TouchingGround;
            isTouchingWall = CollisionSenses.TouchingWall;
            isTouchingLedge = CollisionSenses.LedgeHorizontal;
        }
       
        if(isTouchingWall && !isTouchingLedge)
        {
            mainHero.MainHeroLedgeClimbState.SetDetectedPosition(mainHero.transform.position);
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        xInput = mainHero.PlayerInputHandler.NormalizeInputX;
        yInput = mainHero.PlayerInputHandler.NormalizeInputY;
        grabInput = mainHero.PlayerInputHandler.GrabInput;
        jumpInput = mainHero.PlayerInputHandler.JumpInput;

        if (jumpInput)
        {
            mainHero.MainHeroWallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(mainHero.MainHeroWallJumpState);
        }
        else if (isGrounded && !grabInput)
        {
            stateMachine.ChangeState(mainHero.MainHeroIdleState);
        }
        else if(!isTouchingWall || (xInput != Movement?.FacingDirection && !grabInput))
        {
            stateMachine.ChangeState(mainHero.MainHeroAirState);
        }
        else if(isTouchingWall && !isTouchingLedge)
        {
            stateMachine.ChangeState(mainHero.MainHeroLedgeClimbState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
