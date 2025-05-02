using UnityEngine;

public class ArcherMeleeAttackState : MeleeAttackState
{
    private Archer _archer;
    public ArcherMeleeAttackState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, Transform attackPosition, DataMeleeAttack dataMeleeAttack, Archer archer) : base(entity, stateMachine, animatorBoolName, attackPosition, dataMeleeAttack)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void MakeChecks()
    {
        base.MakeChecks();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (isAnimationFinished)
        {
            if (isMainHeroInMinAgroRange)
            {
                stateMachine.ChangeState(_archer.MainHeroDetectedState);
            }
            else if (!isMainHeroInMinAgroRange)
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
