using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected DataChargeState dataChargeState;

    protected bool isMainHeroInMinAgroRange;
    protected bool isDetectedLedge;
    protected bool isDetectedWall;
    protected bool isChargeTimeOver;
    protected bool performShortRangeAction;
    public ChargeState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataChargeState dataChargeState) : base(entity, stateMachine, animatorBoolName)
    {
        this.dataChargeState = dataChargeState;
    }

    public override void Enter()
    {
        base.Enter();

        isChargeTimeOver = false;
        core.Movement.SetHorizontalVelocity(dataChargeState.ChargeSpeed * core.Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void MakeChecks()
    {
        base.MakeChecks();

        isMainHeroInMinAgroRange = entity.CheckMainHeroInMinAgroRange();
        isDetectedLedge = core.CollisionSenses.LedgeVertical;
        isDetectedWall = core.CollisionSenses.TouchingWall;
        performShortRangeAction = entity.CheckMainHeroInCloseRangeAction();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if(Time.time >= StartTime + dataChargeState.ChargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
