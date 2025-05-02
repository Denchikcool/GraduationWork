public class ArcherDeadState : DeadState
{
    private Archer _archer;
    public ArcherDeadState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataDeadState dataDeadState, Archer archer) : base(entity, stateMachine, animatorBoolName, dataDeadState)
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
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
