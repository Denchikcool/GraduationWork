using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMainHeroDetectedState : MainHeroDetectedState
{
    private Archer _archer;
    public ArcherMainHeroDetectedState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataMainHeroDetected detectedMainHeroData, Archer archer) : base(entity, stateMachine, animatorBoolName, detectedMainHeroData)
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

        if (performShortRangeAction)
        {
            if (Time.time >= _archer.DodgeState.StartTime + _archer.DodgeStateData.DodgeCooldown)
            {
                stateMachine.ChangeState(_archer.DodgeState);
            }
            else
            {
                stateMachine.ChangeState(_archer.MeleeAttackState);
            }
        }
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(_archer.RangeAttackState);
        }
        else if (!isMainHeroInMaxAgroRange)
        {
            stateMachine.ChangeState(_archer.FindMainHeroState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
