using UnityEngine;

public class GameplayState : GameState
{
    public GameplayState(GameStateManager manager) : base(manager) { }

    public override void Enter()
    {
        Debug.Log("Entered Gameplay State");

        UIManager uiManager = stateManager.GetUIManager();
        if (uiManager != null)
        {
            uiManager.ShowGameplayUI();
        }

        Time.timeScale = 1f;

        
        if (LevelManager.Instance != null)
        {
           
            if (LevelManager.Instance.GetRemainingTime() <= 0 ||
                LevelManager.Instance.GetRemainingTime() >= LevelManager.Instance.GetCurrentLevel()?.TimeLimit)
            {
                LevelManager.Instance.StartLevel();
            }
        }

        EnablePlayerInput(true);
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        Debug.Log("Exiting Gameplay State");

        
        EnablePlayerInput(false);

        
        UIManager uiManager = stateManager.GetUIManager();
        if (uiManager != null)
        {
            uiManager.HideGameplayUI();
        }
    }

    private void EnablePlayerInput(bool enabled)
    {

        PlayerController player = GameObject.FindObjectOfType<PlayerController>();

        if (player != null)
        {
            player.enabled = enabled;
        }


        TelekinesisController telekinesis = GameObject.FindObjectOfType<TelekinesisController>();
        if (telekinesis != null)
        {
            telekinesis.enabled = enabled;
        }
    }
}