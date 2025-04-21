using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMeleeAttackState : MeleeAttackState
{
    private Golem _golem;
    public GolemMeleeAttackState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, Transform attackPosition, DataMeleeAttack dataMeleeAttack, Golem golem) : base(entity, stateMachine, animatorBoolName, attackPosition, dataMeleeAttack)
    {
        this._golem = golem;
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
                stateMachine.ChangeState(_golem.MainHeroDetectedState);
            }
            else
            {
                stateMachine.ChangeState(_golem.FindMainHeroState);
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
