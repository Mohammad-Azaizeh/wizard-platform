using UnityEngine;

public class TelekinesisPushingState : TelekinesisState
{
    public TelekinesisPushingState(TelekinesisManager manager) : base(manager) { }

    public override void Enter()
    {
        Debug.Log("Telekinesis: Pushing State");
        manager.ShowPushEffect();
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
            manager.ApplyPushForce();
        }
    }

    public override void Exit()
    {
        manager.HidePushEffect();
    }

    public override void OnPushCanceled()
    {
        manager.ChangeState(manager.GetIdleState());
    }
}
