using UnityEngine;


public class MenuState : GameState
{
    public MenuState(GameStateManager manager) : base(manager) { }

    public override void Enter()
    {
        Debug.Log("Entered Menu State");

        
        UIManager uiManager = stateManager.GetUIManager();
        if (uiManager != null)
        {
            uiManager.ShowMenuUI();
        }

        
        Time.timeScale = 1f;
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        Debug.Log("Exiting Menu State");

        
        UIManager uiManager = stateManager.GetUIManager();
        if (uiManager != null)
        {
            uiManager.HideMenuUI();
        }
    }
}
