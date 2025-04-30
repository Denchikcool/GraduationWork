using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainHeroDashState : MainHeroAbilityState
{
    private float _lastDashTime;

    private bool _isHolding;
    private bool _dashInputStop;

    private Vector2 _dashDirection;
    private Vector2 _dashDirectionInput;
    private Vector2 _lastAfterImagePosition;

    public bool CanDash { get; private set; }

    public MainHeroDashState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        mainHero.PlayerInputHandler.ChangeDashInput();

        _isHolding = true;
        _dashDirection = Vector2.right * Movement.FacingDirection;

        Time.timeScale = mainHeroData.HoldTimeScale;
        startTime = Time.unscaledTime;

        mainHero.DashArrow.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        if(Movement?.CurrentVelocity.y > 0)
        {
            Movement?.SetVerticalVelocity(Movement.CurrentVelocity.y * mainHeroData.DashEndVerticalMultiplier);
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (!isExitingState)
        {
            mainHero.Animator.SetFloat("yVelocity", Movement.CurrentVelocity.y);
            mainHero.Animator.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));

            if (_isHolding)
            {
                _dashDirectionInput = mainHero.PlayerInputHandler.DashDirectionInput;
                _dashInputStop = mainHero.PlayerInputHandler.DashInputStop;

                if(_dashDirectionInput != Vector2.zero)
                {
                    _dashDirection = _dashDirectionInput;
                    _dashDirection.Normalize();
                }

                float angle = Vector2.SignedAngle(Vector2.right, _dashDirection);
                mainHero.DashArrow.rotation = Quaternion.Euler(0.0f, 0.0f, angle - 45.0f);

                if(_dashInputStop || Time.unscaledTime >= startTime + mainHeroData.MaxHoldTime)
                {
                    _isHolding = false;
                    Time.timeScale = 1.0f;
                    startTime = Time.time;
                    Movement?.CheckShouldFlip(Mathf.RoundToInt(_dashDirection.x));
                    mainHero.Rigidbody.drag = mainHeroData.Drag;
                    Movement?.SetVelocity(mainHeroData.DashVelocity, _dashDirection);
                    mainHero.DashArrow.gameObject.SetActive(false);
                    PlaceAfterImage();
                }
            }
            else
            {
                Movement?.SetVelocity(mainHeroData.DashVelocity, _dashDirection);
                CheckIfShouldPlaceAfterImage();

                if (Time.time >= startTime + mainHeroData.DashTime)
                {
                    mainHero.Rigidbody.drag = 0.0f;
                    isAbilityDone = true;
                    _lastDashTime = Time.time;
                }
            }
        }
    }

    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= _lastDashTime + mainHeroData.DashCooldown;
    }

    public void ResetCanDash()
    {
        CanDash = true;
    }

    private void PlaceAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        _lastAfterImagePosition = mainHero.transform.position;
    }

    private void CheckIfShouldPlaceAfterImage()
    {
        if(Vector2.Distance(mainHero.transform.position, _lastAfterImagePosition) >= mainHeroData.DistanceBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }
}
