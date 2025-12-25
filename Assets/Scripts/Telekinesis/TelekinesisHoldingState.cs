using UnityEngine;

public class TelekinesisHoldingState : TelekinesisState
{
    private float holdTimer;

    public TelekinesisHoldingState(TelekinesisManager manager) : base(manager) { }

    public override void Enter()
    {
        Debug.Log("Telekinesis: Holding State");

        holdTimer = 0f;
        manager.GrabCurrentTarget();
        manager.ShowHoldEffect();
    }

    public override void Update()
    {
        if (!manager.HasCurrentTarget())
        {
            manager.ChangeState(manager.GetIdleState());
            return;
        }

        holdTimer += Time.deltaTime;

        float maxHoldTime = manager.GetAbilityData().MaxHoldTime;
        if (holdTimer >= maxHoldTime)
        {
            Debug.Log("Max hold time reached");
            manager.ChangeState(manager.GetIdleState());
            return;
        }
    }

    public override void FixedUpdate()
    {
        if (manager.HasCurrentTarget())
        {
            manager.HoldObjectAtPosition();
        }
    }

    public override void Exit()
    {
        manager.ReleaseCurrentTarget();
        manager.HideHoldEffect();
    }

    public override void OnHoldCanceled()
    {
        manager.ChangeState(manager.GetIdleState());
    }

    public override void OnPushStarted()
    {
        manager.ApplyPushForce();
        manager.ChangeState(manager.GetIdleState());
    }
}
