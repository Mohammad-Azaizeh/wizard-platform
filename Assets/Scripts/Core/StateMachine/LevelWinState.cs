using UnityEngine;


public class LevelWinState : GameState
{
    public LevelWinState(GameStateManager manager) : base(manager) { }

    public override void Enter()
    {
        Debug.Log("Entered Level Win State");

        
        Time.timeScale = 0f;

        
        UIManager uiManager = stateManager.GetUIManager();
        if (uiManager != null)
        {
            uiManager.ShowLevelWinUI();
        }

        
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        Debug.Log("Exiting Level Win State");

        
        Time.timeScale = 1f;

        
        UIManager uiManager = stateManager.GetUIManager();
        if (uiManager != null)
        {
            uiManager.HideLevelWinUI();
        }
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;

        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.LoadNextLevel();
        }

        stateManager.StartGameplay();
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
