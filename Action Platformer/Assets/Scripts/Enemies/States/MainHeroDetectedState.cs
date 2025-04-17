using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroDetectedState : State
{
    protected DataMainHeroDetected detectedMainHeroData;

    protected bool isMainHeroInMinAgroRange;
    protected bool isMainHeroInMaxAgroRange;
    public MainHeroDetectedState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataMainHeroDetected detectedMainHeroData) : base(entity, stateMachine, animatorBoolName)
    {
        this.detectedMainHeroData = detectedMainHeroData;
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(0.0f);
        isMainHeroInMinAgroRange = entity.CheckMainHeroInMinAgroRange();
        isMainHeroInMaxAgroRange = entity.CheckMainHeroInMaxAgroRange();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        isMainHeroInMinAgroRange = entity.CheckMainHeroInMinAgroRange();
        isMainHeroInMaxAgroRange = entity.CheckMainHeroInMaxAgroRange();
    }
}
