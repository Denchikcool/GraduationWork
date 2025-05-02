public class MainHeroWallClimbState : MainHeroTouchWallState
{
    public MainHeroWallClimbState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (!isExitingState)
        {
            Movement?.SetVerticalVelocity(mainHeroData.WallClimbVelocity);

            if (yInput != 1)
            {
                stateMachine.ChangeState(mainHero.MainHeroWallGrabState);
            }
        }
    }
}
