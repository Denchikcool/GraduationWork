using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherRangeAttackState : RangeAttackState
{
    private Archer _archer;
    public ArcherRangeAttackState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, Transform attackPosition, DataRangeAttackState dataRangeAttackState, Archer archer) : base(entity, stateMachine, animatorBoolName, attackPosition, dataRangeAttackState)
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
