using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected DataWeapon _weaponData;

    protected Animator mainHeroMovementAnimator;
    protected Animator weaponAnimator;

    protected MainHeroAttackState mainHeroAttackState;
    protected Core core;

    protected int attackCounter;

    protected virtual void Awake()
    {
        mainHeroMovementAnimator = transform.Find("MainHeroMovement").GetComponent<Animator>();
        weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();

        gameObject.SetActive(false);
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        if(attackCounter >= _weaponData.AmountOfAttacks)
        {
            attackCounter = 0;
        }

        mainHeroMovementAnimator.SetBool("attack", true);
        weaponAnimator.SetBool("attack", true);

        mainHeroMovementAnimator.SetInteger("attackCounter", attackCounter);
        weaponAnimator.SetInteger("attackCounter", attackCounter);
    }

    public virtual void ExitWeapon()
    {
        mainHeroMovementAnimator.SetBool("attack", false);
        weaponAnimator.SetBool("attack", false);

        attackCounter++;

        gameObject.SetActive(false);
    }

    #region Animation Triggers
    public virtual void AnimationFinishTrigger()
    {
        mainHeroAttackState.AnimationFinishTrigger();
    }

    public virtual void AnimationStartMovementTrigger()
    {
        mainHeroAttackState.SetMainHeroVelocity(_weaponData.MovementSpeed[attackCounter]);
    }

    public virtual void AnimationStopMovementTrigger()
    {
        mainHeroAttackState.SetMainHeroVelocity(0.0f);
    }

    public virtual void AnimationTurnOffFlipTrigger()
    {
        mainHeroAttackState.SetFlipCheck(false);
    }

    public virtual void AnimationTurnOnFlipTrigger()
    {
        mainHeroAttackState.SetFlipCheck(true);
    }

    public virtual void AnimationActionTrigger()
    {

    }
    #endregion

    public void InitializeWeapon(MainHeroAttackState mainHeroAttackState, Core core)
    {
        this.mainHeroAttackState = mainHeroAttackState;
        this.core = core;
    }
}
