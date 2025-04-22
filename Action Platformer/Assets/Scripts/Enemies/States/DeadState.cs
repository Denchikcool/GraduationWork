using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    protected DataDeadState dataDeadState;
    public DeadState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataDeadState dataDeadState) : base(entity, stateMachine, animatorBoolName)
    {
        this.dataDeadState = dataDeadState;
    }

    public override void Enter()
    {
        base.Enter();

        GameObject.Instantiate(dataDeadState.DeathBloodParticle, entity.AliveGameObject.transform.position, dataDeadState.DeathBloodParticle.transform.rotation);
        GameObject.Instantiate(dataDeadState.DeathChunkParticle, entity.AliveGameObject.transform.position, dataDeadState.DeathChunkParticle.transform.rotation);

        entity.gameObject.SetActive(false);
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
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
