using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroGroundedState : MainHeroState
{
    protected int inputXPosition;
    public MainHeroGroundedState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void MakeChecks()
    {
        base.MakeChecks();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        inputXPosition = mainHero.PlayerInputHandler.NormalizeInputX;
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
