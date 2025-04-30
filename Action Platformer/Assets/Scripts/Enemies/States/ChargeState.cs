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

    private Movement _movement;
    private CollisionSenses _collisionSenses;

    private Movement Movement
    {
        get => _movement ?? core.GetCoreComponent(ref _movement);
    }

    private CollisionSenses CollisionSenses
    {
        get => _collisionSenses ?? core.GetCoreComponent(ref _collisionSenses);
    }

    public ChargeState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataChargeState dataChargeState) : base(entity, stateMachine, animatorBoolName)
    {
        this.dataChargeState = dataChargeState;
    }

    public override void Enter()
    {
        base.Enter();

        isChargeTimeOver = false;
        Movement?.SetHorizontalVelocity(dataChargeState.ChargeSpeed * Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void MakeChecks()
    {
        base.MakeChecks();

        isMainHeroInMinAgroRange = entity.CheckMainHeroInMinAgroRange();
        isDetectedLedge = CollisionSenses.LedgeVertical;
        isDetectedWall = CollisionSenses.TouchingWall;
        performShortRangeAction = entity.CheckMainHeroInCloseRangeAction();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        Movement?.SetHorizontalVelocity(dataChargeState.ChargeSpeed * Movement.FacingDirection);

        if (Time.time >= StartTime + dataChargeState.ChargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
