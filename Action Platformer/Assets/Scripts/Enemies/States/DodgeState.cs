using UnityEngine;
using Denchik.CoreSystem;

public class DodgeState : State
{
    protected DataDodgeState dataDodgeState;

    protected bool performCloseRangeAction;
    protected bool isMainHeroInMaxAgroRange;
    protected bool isGrounded;
    protected bool isDodgeOver;

    private Movement _movement;
    private CollisionSenses _collisionSenses;

    private Movement Movement
    {
        get => _movement ?? core.GetCoreComponent(ref _movement);
    }

    private CollisionSenses CollisionSenses
    {
        get => _collisionSenses ?? core.GetCoreComponent(ref _collisionSenses);
    }
    public DodgeState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataDodgeState dataDodgeState) : base(entity, stateMachine, animatorBoolName)
    {
        this.dataDodgeState = dataDodgeState;
    }

    public override void Enter()
    {
        base.Enter();

        isDodgeOver = false;
        Movement?.SetVelocity(dataDodgeState.DodgeSpeed, dataDodgeState.DodgeAngle, -Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void MakeChecks()
    {
        base.MakeChecks();

        performCloseRangeAction = entity.CheckMainHeroInCloseRangeAction();
        isMainHeroInMaxAgroRange = entity.CheckMainHeroInMaxAgroRange();
        isGrounded = CollisionSenses.TouchingGround;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if(Time.time >= StartTime + dataDodgeState.DodgeTime && isGrounded)
        {
            isDodgeOver = true;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
