using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMainHeroState : State
{
    protected DataFindMainHero dataFindMainHero;

    protected bool turnNow;
    protected bool isMainHeroInMinAgroRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;

    protected float lastTurnTime;

    protected int CountOfTurnsDone;

    public FindMainHeroState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataFindMainHero dataFindMainHero) : base(entity, stateMachine, animatorBoolName)
    {
        this.dataFindMainHero = dataFindMainHero;
    }

    public override void Enter()
    {
        base.Enter();

        isAllTurnsDone = false;
        isAllTurnsTimeDone = false;
        lastTurnTime = StartTime;
        CountOfTurnsDone = 0;
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

        if (turnNow)
        {
            core.Movement.Flip();
            lastTurnTime = Time.time;
            CountOfTurnsDone++;
            turnNow = false;
        }
        else if (Time.time >= lastTurnTime + dataFindMainHero.TimeBetweenTurns && !isAllTurnsDone)
        {
            core.Movement.Flip();
            lastTurnTime = Time.time;
            CountOfTurnsDone++;
        }

        if(CountOfTurnsDone >= dataFindMainHero.CountOfTurns)
        {
            isAllTurnsDone = true;
        }

        if(Time.time >= lastTurnTime + dataFindMainHero.TimeBetweenTurns && isAllTurnsDone)
        {
            isAllTurnsTimeDone = true;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    public void SetTurnNow(bool turn)
    {
        turnNow = turn;
    }
}
