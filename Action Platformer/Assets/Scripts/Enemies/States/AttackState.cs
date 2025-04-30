using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Transform attackPosition;

    protected bool isAnimationFinished;
    protected bool isMainHeroInMinAgroRange;
    public AttackState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, Transform attackPosition) : base(entity, stateMachine, animatorBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void Enter()
    {
        base.Enter();

        entity.AnimationToStateMachine.AttackState = this;
        isAnimationFinished = false;
        core.Movement.SetHorizontalVelocity(0.0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void MakeChecks()
    {
        base.MakeChecks();

        isMainHeroInMinAgroRange = entity.CheckMainHeroInMinAgroRange();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        core.Movement.SetHorizontalVelocity(0.0f);
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    public virtual void TriggerAttack()
    {

    }

    public virtual void FinishAttack()
    {
        isAnimationFinished = true;
    }
}
