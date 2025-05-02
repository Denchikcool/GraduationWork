public class ArcherDodgeState : DodgeState
{
    private Archer _archer;
    public ArcherDodgeState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataDodgeState dataDodgeState, Archer archer) : base(entity, stateMachine, animatorBoolName, dataDodgeState)
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

        if (isDodgeOver)
        {
            if(isMainHeroInMaxAgroRange && performCloseRangeAction)
            {
                stateMachine.ChangeState(_archer.MeleeAttackState);
            }
            else if(isMainHeroInMaxAgroRange && !performCloseRangeAction)
            {
                stateMachine.ChangeState(_archer.RangeAttackState);
            }
            else if (!isMainHeroInMaxAgroRange)
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
