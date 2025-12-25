using UnityEngine;

public class TelekinesisPullingState : TelekinesisState
{
    public TelekinesisPullingState(TelekinesisManager manager) : base(manager) { }

    public override void Enter()
    {
        Debug.Log("Telekinesis: Pulling State");
        manager.ShowPullEffect();
    }

    public override void Update()
    {
        if (!manager.HasCurrentTarget())
        {
            manager.ChangeState(manager.GetIdleState());
            return;
        }

        if (!manager.IsTargetInRange())
        {
            manager.ChangeState(manager.GetIdleState());
            return;
        }
    }

    public override void FixedUpdate()
    {
        if (manager.HasCurrentTarget())
        {
            manager.ApplyPullForce();
        }
    }

    public override void Exit()
    {
        manager.HidePullEffect();
    }

    public override void OnPullCanceled()
    {
        manager.ChangeState(manager.GetIdleState());
    }

    public override void OnHoldStarted()
    {
        manager.ChangeState(manager.GetHoldingState());
    }
}
