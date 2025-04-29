using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FinalStateMachine stateMachine;
    protected Entity entity;
    protected Core core;

    public float StartTime { get; protected set; }

    protected string animatorBoolName;

    public State(Entity entity, FinalStateMachine stateMachine, string animatorBoolName)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animatorBoolName = animatorBoolName;
        core = entity.Core;
    }

    public virtual void Enter()
    {
        StartTime = Time.time;
        entity.Animator.SetBool(animatorBoolName, true);
        MakeChecks();
    }

    public virtual void Exit()
    {
        entity.Animator.SetBool(animatorBoolName, false);
    }

    public virtual void UpdateLogic()
    {

    }

    public virtual void UpdatePhysics()
    {
        MakeChecks();
    }

    public virtual void MakeChecks()
    {

    }
}
