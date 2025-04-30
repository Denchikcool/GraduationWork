using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroWallSlideState : MainHeroTouchWallState
{
    public MainHeroWallSlideState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (!isExitingState)
        {
            Movement?.SetVerticalVelocity(-mainHeroData.WallSlideVelocity);

            if (grabInput && yInput == 0)
            {
                stateMachine.ChangeState(mainHero.MainHeroWallGrabState);
            }
        } 
    }
}
