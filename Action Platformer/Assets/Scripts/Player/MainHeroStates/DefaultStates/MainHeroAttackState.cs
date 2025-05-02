using Denchik.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroAttackState : MainHeroAbilityState
{
    private Weapon _weapon;
    public MainHeroAttackState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName, Weapon weapon) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
        this._weapon = weapon;
        _weapon.OnExit += ExitHandler;
    }

    public override void Enter()
    {
        base.Enter();

        _weapon.Enter();
    }

    private void ExitHandler()
    {
        AnimationFinishTrigger();
        isAbilityDone = true;
    }

}
