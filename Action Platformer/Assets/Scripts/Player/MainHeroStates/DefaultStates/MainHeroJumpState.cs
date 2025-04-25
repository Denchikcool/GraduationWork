using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        mainHero.SetVerticalVelocity(mainHeroData.JumpVelocity);
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
