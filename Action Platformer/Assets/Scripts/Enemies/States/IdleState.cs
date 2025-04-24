using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected DataIdleState dataIdleState;

    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    protected bool isMainHeroInMinAgroRange;

    protected float idleTime;
    public IdleState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataIdleState dataIdleState) : base(entity, stateMachine, animatorBoolName)
    {
        this.dataIdleState = dataIdleState;
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(0);
        isIdleTimeOver = false;
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        if (flipAfterIdle)
        {
            entity.Flip();
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if(Time.time >= StartTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    public void SetFlipAfterIdle(bool flipAfterIdle)
    {
        this.flipAfterIdle = flipAfterIdle;
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(dataIdleState.MinIdleTime, dataIdleState.MaxIdleTime);
    }

    public override void MakeChecks()
    {
        base.MakeChecks();

        isMainHeroInMinAgroRange = entity.CheckMainHeroInMinAgroRange();
    }
}
