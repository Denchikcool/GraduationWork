public class ArcherIdleState : IdleState
{
    private Archer _archer;
    public ArcherIdleState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataIdleState dataIdleState, Archer archer) : base(entity, stateMachine, animatorBoolName, dataIdleState)
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
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(_archer.MoveState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
