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

    private Movement _movement;
    private CollisionSenses _collisionSenses;

    protected Movement Movement
    {
        get => _movement ?? core.GetCoreComponent(ref _movement);
    }

    private CollisionSenses CollisionSenses
    {
        get => _collisionSenses ?? core.GetCoreComponent(ref _collisionSenses);
    }

    public MainHeroDetectedState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataMainHeroDetected detectedMainHeroData) : base(entity, stateMachine, animatorBoolName)
    {
        this.detectedMainHeroData = detectedMainHeroData;
    }

    public override void Enter()
    {
        base.Enter();

        performLongRangeAction = false;
        Movement?.SetHorizontalVelocity(0.0f);
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
        isDetectedLedge = CollisionSenses.LedgeVertical;
        performShortRangeAction = entity.CheckMainHeroInCloseRangeAction();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        Movement?.SetHorizontalVelocity(0.0f);

        if (Time.time >= StartTime + detectedMainHeroData.LongRangeActionTime)
        {
            performLongRangeAction = true;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
