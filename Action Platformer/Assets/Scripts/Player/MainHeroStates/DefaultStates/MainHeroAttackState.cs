using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroAttackState : MainHeroAbilityState
{

    public MainHeroAttackState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
    }

}
