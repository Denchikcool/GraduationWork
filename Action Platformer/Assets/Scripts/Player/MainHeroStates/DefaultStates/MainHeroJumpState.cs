public class MainHeroJumpState : MainHeroAbilityState
{
    private int _amountOfJumpsLeft;
    public MainHeroJumpState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
        _amountOfJumpsLeft = mainHeroData.AmountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        mainHero.PlayerInputHandler.ChangeJumpInput();
        Movement?.SetVerticalVelocity(mainHeroData.JumpVelocity);
        SoundEffectManager.PlaySound("Jumping");
        isAbilityDone = true;
        _amountOfJumpsLeft--;
        mainHero.MainHeroAirState.SetIsJumping();  
    }

    public bool CanJump()
    {
        if(_amountOfJumpsLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAmountOfJumpsLeft()
    {
        _amountOfJumpsLeft = mainHeroData.AmountOfJumps;
    }

    public void DecreaseAmountOfJumpsLeft()
    {
        _amountOfJumpsLeft--;
    }
}
