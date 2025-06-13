using UnityEngine;
public class MainHeroMoveState : MainHeroGroundedState
{
    /*private bool _playingSteps;

    [SerializeField]
    private float _stepsSpeed = 0.5f;*/

    public MainHeroMoveState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void MakeChecks()
    {
        base.MakeChecks();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        Movement?.CheckShouldFlip(inputXPosition);
        Movement?.SetHorizontalVelocity(mainHeroData.MovementVelocity * inputXPosition);
        SoundEffectManager.PlaySound("Walking");

        if (!isExitingState)
        {
            if (inputXPosition == 0)
            {
                stateMachine.ChangeState(mainHero.MainHeroIdleState);
            }
            else if (inputYPosition == -1)
            {
                stateMachine.ChangeState(mainHero.MainHeroCrouchMoveState);
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
    
    /*public override void StartFootsteps()
    {
        _playingSteps = true;
        InvokeRepeating(nameof(PlayFootstep), 0.0f, _stepsSpeed);
        
    }

    public override void StopFootsteps()
    {
        _playingSteps = false;
        CancelInvoke(nameof(PlayFootstep));
    }

    private void PlayFootstep()
    {
        SoundEffectManager.PlaySound("Walking");
    }*/
}
