using Denchik.CoreSystem;

public class MoveState : State
{
    protected DataMoveState dataMoveState;

    protected bool isDetectedWall;
    protected bool isDetectedLedge;
    protected bool isMainHeroInMinAgroRange;

    private Movement _movement;
    private CollisionSenses _collisionSenses;

    private Movement Movement
    {
        get => _movement ?? core.GetCoreComponent(ref _movement);
    }

    private CollisionSenses CollisionSenses
    {
        get => _collisionSenses ?? core.GetCoreComponent(ref _collisionSenses);
    }
    public MoveState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataMoveState dataMoveState) : base(entity, stateMachine, animatorBoolName)
    {
        this.dataMoveState = dataMoveState;
    }

    public override void Enter()
    {
        base.Enter();
        Movement?.SetHorizontalVelocity(dataMoveState.MovementSpeed * Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void MakeChecks()
    {
        base.MakeChecks();

        isDetectedLedge = CollisionSenses.LedgeVertical;
        isDetectedWall = CollisionSenses.TouchingWall;
        isMainHeroInMinAgroRange = entity.CheckMainHeroInMinAgroRange();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        Movement?.SetHorizontalVelocity(dataMoveState.MovementSpeed * Movement.FacingDirection);
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
