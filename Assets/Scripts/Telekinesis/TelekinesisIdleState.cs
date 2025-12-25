using UnityEngine;

public class TelekinesisIdleState : TelekinesisState
{
    public TelekinesisIdleState(TelekinesisManager manager) : base(manager) { }

    public override void Enter()
    {
        Debug.Log("Telekinesis: Idle State");
    }

    public override void Update()
    {
        Debug.Log("IdleState Update running");
        manager.UpdateTargetScanning();
    }

    public override void FixedUpdate()
    {
    }

    public override void Exit()
    {
    }

    public override void OnPullStarted()
    {
        if (manager.HasCurrentTarget())
        {
            manager.ChangeState(manager.GetPullingState());
        }
    }

    public override void OnPushStarted()
    {
        if (manager.HasCurrentTarget())
        {
            manager.ChangeState(manager.GetPushingState());
        }
    }

    public override void OnHoldStarted()
    {
        if (manager.HasCurrentTarget())
        {
            manager.ChangeState(manager.GetHoldingState());
        }
    }
}
