using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroLandState : MainHeroGroundedState
{
    public MainHeroLandState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (!isExitingState)
        {
            if (inputXPosition != 0)
            {
                stateMachine.ChangeState(mainHero.MainHeroMoveState);
            }
            else if (isAnimationFinish)
            {
                stateMachine.ChangeState(mainHero.MainHeroIdleState);
            }
        }
    }
}
