using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : State
{
    protected DataDodgeState dataDodgeState;

    protected bool performCloseRangeAction;
    protected bool isMainHeroInMaxAgroRange;
    protected bool isGrounded;
    protected bool isDodgeOver;
    public DodgeState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataDodgeState dataDodgeState) : base(entity, stateMachine, animatorBoolName)
    {
        this.dataDodgeState = dataDodgeState;
    }

    public override void Enter()
    {
        base.Enter();

        isDodgeOver = false;
        entity.SetVelocity(dataDodgeState.DodgeSpeed, dataDodgeState.DodgeAngle, -entity.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void MakeChecks()
    {
        base.MakeChecks();

        performCloseRangeAction = entity.CheckMainHeroInCloseRangeAction();
        isMainHeroInMaxAgroRange = entity.CheckMainHeroInMaxAgroRange();
        isGrounded = entity.CheckGround();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if(Time.time >= StartTime + dataDodgeState.DodgeTime && isGrounded)
        {
            isDodgeOver = true;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
