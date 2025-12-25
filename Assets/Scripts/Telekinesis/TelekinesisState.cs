using UnityEngine;

public abstract class TelekinesisState
{
    protected TelekinesisManager manager;

    public TelekinesisState(TelekinesisManager manager)
    {
        this.manager = manager;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void Exit();

    public virtual void OnPullStarted() { }
    public virtual void OnPullCanceled() { }
    public virtual void OnPushStarted() { }
    public virtual void OnPushCanceled() { }
    public virtual void OnHoldStarted() { }
    public virtual void OnHoldCanceled() { }
    public virtual void OnRotateClockwise() { }
    public virtual void OnRotateCounterClockwise() { }
}
