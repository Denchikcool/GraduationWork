using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroState
{
    protected MainHero mainHero;
    protected MainHeroStateMachine stateMachine;
    protected MainHeroData mainHeroData;

    protected float startTime;

    protected bool isAnimationFinish;

    private string _animationBoolName;

    public MainHeroState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName)
    {
        this.mainHero = mainHero;
        this.stateMachine = stateMachine;
        this.mainHeroData = mainHeroData;
        this._animationBoolName = animationBoolName;
    }

    public virtual void Enter()
    {
        MakeChecks();
        mainHero.Animator.SetBool(_animationBoolName, true);
        startTime = Time.time;
        Debug.Log(_animationBoolName);
        isAnimationFinish = false;
    }

    public virtual void Exit()
    {
        mainHero.Animator.SetBool(_animationBoolName, false);
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

    public virtual void AnimationTrigger()
    {

    }

    public virtual void AnimationFinishTrigger()
    {
        isAnimationFinish = true;
    }
}
