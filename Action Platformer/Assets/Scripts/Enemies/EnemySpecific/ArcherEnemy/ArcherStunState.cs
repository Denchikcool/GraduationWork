public class ArcherStunState : StunState
{
    private Archer _archer;
    public ArcherStunState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataStunState dataStunState, Archer archer) : base(entity, stateMachine, animatorBoolName, dataStunState)
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

        if (isStunTimeOver)
        {
            if (isMainHeroInMinAgroRange)
            {
                stateMachine.ChangeState(_archer.MainHeroDetectedState);
            }
            else
            {
                stateMachine.ChangeState(_archer.FindMainHeroState);
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
