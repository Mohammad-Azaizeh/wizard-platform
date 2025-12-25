using UnityEngine;


public abstract class GameState
{
    protected GameStateManager stateManager;

    public GameState(GameStateManager manager)
    {
        stateManager = manager;
    }

    
    public abstract void Enter();

    
    public abstract void Update();

    
    public abstract void Exit();
}