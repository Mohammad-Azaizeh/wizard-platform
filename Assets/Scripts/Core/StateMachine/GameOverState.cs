using UnityEngine;


public class GameOverState : GameState
{
    public GameOverState(GameStateManager manager) : base(manager) { }

    public override void Enter()
    {
        Debug.Log("Entered Game Over State");

        
        Time.timeScale = 0f;

        
        UIManager uiManager = stateManager.GetUIManager();
        if (uiManager != null)
        {
            uiManager.ShowGameOverUI();
        }

        
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        Debug.Log("Exiting Game Over State");

        
        Time.timeScale = 1f;

        
        UIManager uiManager = stateManager.GetUIManager();
        if (uiManager != null)
        {
            uiManager.HideGameOverUI();
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;

        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.RestartLevel();
        }

        stateManager.StartGameplay();
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        stateManager.GoToMenu();
    }
}
