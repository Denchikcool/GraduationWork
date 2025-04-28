using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Animator mainHeroMovementAnimator;
    protected Animator weaponAnimator;

    protected MainHeroAttackState mainHeroAttackState;

    protected virtual void Start()
    {
        mainHeroMovementAnimator = transform.Find("MainHeroMovement").GetComponent<Animator>();
        weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();

        gameObject.SetActive(false);
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        mainHeroMovementAnimator.SetBool("attack", true);
        weaponAnimator.SetBool("attack", true);
    }

    public virtual void ExitWeapon()
    {
        mainHeroMovementAnimator.SetBool("attack", false);
        weaponAnimator.SetBool("attack", false);

        gameObject.SetActive(false);
    }

    #region Animation Triggers
    public virtual void AnimationFinishTrigger()
    {
        mainHeroAttackState.AnimationFinishTrigger();
    }
    #endregion

    public void InitializeWeapon(MainHeroAttackState mainHeroAttackState)
    {
        this.mainHeroAttackState = mainHeroAttackState;
    }
}
