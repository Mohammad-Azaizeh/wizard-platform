using UnityEngine;


public class PauseState : GameState
{
    public PauseState(GameStateManager manager) : base(manager) { }

    public override void Enter()
    {
        Debug.Log("Entered Pause State");

        
        Time.timeScale = 0f;

        
        UIManager uiManager = stateManager.GetUIManager();
        if (uiManager != null)
        {
            uiManager.ShowPauseUI();
        }
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        Debug.Log("Exiting Pause State");

        
        Time.timeScale = 1f;

        
        UIManager uiManager = stateManager.GetUIManager();
        if (uiManager != null)
        {
            uiManager.HidePauseUI();
        }
    }

    public void ResumeGame()
    {
        stateManager.ChangeState(stateManager.GetGameplayState());
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f; 
        stateManager.GoToMenu();
    }
}
