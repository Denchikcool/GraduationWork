using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroDetectedState : State
{
    protected DataMainHeroDetected detectedMainHeroData;

    protected bool isMainHeroInMinAgroRange;
    protected bool isMainHeroInMaxAgroRange;
    protected bool performLongRangeAction;
    protected bool performShortRangeAction;
    protected bool isDetectedLedge;
    public MainHeroDetectedState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataMainHeroDetected detectedMainHeroData) : base(entity, stateMachine, animatorBoolName)
    {
        this.detectedMainHeroData = detectedMainHeroData;
    }

    public override void Enter()
    {
        base.Enter();

        performLongRangeAction = false;
        entity.SetVelocity(0.0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void MakeChecks()
    {
        base.MakeChecks();

        isMainHeroInMinAgroRange = entity.CheckMainHeroInMinAgroRange();
        isMainHeroInMaxAgroRange = entity.CheckMainHeroInMaxAgroRange();
        isDetectedLedge = entity.CheckLedge();
        performShortRangeAction = entity.CheckMainHeroInCloseRangeAction();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if(Time.time >= startTime + detectedMainHeroData.LongRangeActionTime)
        {
            performLongRangeAction = true;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
