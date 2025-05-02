public class ArcherMoveState : MoveState
{
    private Archer _archer;
    public ArcherMoveState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataMoveState dataMoveState, Archer archer) : base(entity, stateMachine, animatorBoolName, dataMoveState)
    {
        this._archer = archer;
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
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (isMainHeroInMinAgroRange)
        {
            stateMachine.ChangeState(_archer.MainHeroDetectedState);
        }
        else if(isDetectedWall || !isDetectedLedge)
        {
            _archer.IdleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(_archer.IdleState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
