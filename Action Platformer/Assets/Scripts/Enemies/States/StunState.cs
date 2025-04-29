using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{
    protected DataStunState dataStunState;

    protected bool isStunTimeOver;
    protected bool isGrounded;
    protected bool isMovementStopped;
    protected bool performCloseRangeAction;
    protected bool isMainHeroInMinAgroRange;
    public StunState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataStunState dataStunState) : base(entity, stateMachine, animatorBoolName)
    {
        this.dataStunState = dataStunState;
    }

    public override void Enter()
    {
        base.Enter();

        isStunTimeOver = false;
        isMovementStopped = false;
        core.Movement.SetVelocity(dataStunState.StunKnockbackSpeed, dataStunState.StunKnockbackAngle, entity.LastDamageDirection);
    }

    public override void Exit()
    {
        base.Exit();

        entity.ResetStunResistance();
    }

    public override void MakeChecks()
    {
        base.MakeChecks();

        isGrounded = core.CollisionSenses.TouchingGround;
        performCloseRangeAction = entity.CheckMainHeroInCloseRangeAction();
        isMainHeroInMinAgroRange = entity.CheckMainHeroInMinAgroRange();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if(Time.time >= StartTime + dataStunState.StunTime)
        {
            isStunTimeOver = true;
        }

        if(isGrounded && Time.time >= StartTime + dataStunState.StunKnockbackTime && !isMovementStopped)
        {
            isMovementStopped = true;
            core.Movement.SetHorizontalVelocity(0.0f);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
