using UnityEngine;
using TMPro;


public class UIManager : MonoBehaviour
{
    [Header("UI Screens")]
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private GameObject gameplayScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject levelWinScreen;
    [SerializeField] private GameObject gameOverScreen;

    [Header("Gameplay UI Elements")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI levelNumberText;

    [Header("Win Screen")]
    [SerializeField] private TextMeshProUGUI winTimeText;

    private void Start()
    {
        HideAllScreens();
    }

    private void Update()
    {
       
        if (gameplayScreen != null && gameplayScreen.activeSelf && LevelManager.Instance != null)
        {
            UpdateTimerDisplay();
        }
    }

    private void HideAllScreens()
    {
        if (menuScreen != null) menuScreen.SetActive(false);
        if (gameplayScreen != null) gameplayScreen.SetActive(false);
        if (pauseScreen != null) pauseScreen.SetActive(false);
        if (levelWinScreen != null) levelWinScreen.SetActive(false);
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
    }

    
    public void ShowMenuUI()
    {
        HideAllScreens();
        if (menuScreen != null) menuScreen.SetActive(true);
    }

    public void HideMenuUI()
    {
        if (menuScreen != null) menuScreen.SetActive(false);
    }

    
    public void ShowGameplayUI()
    {
        HideAllScreens();
        if (gameplayScreen != null) gameplayScreen.SetActive(true);
        UpdateLevelNumber();
    }

    public void HideGameplayUI()
    {
        if (gameplayScreen != null) gameplayScreen.SetActive(false);
    }

    
    public void ShowPauseUI()
    {
        if (pauseScreen != null) pauseScreen.SetActive(true);
    }

    public void HidePauseUI()
    {
        if (pauseScreen != null) pauseScreen.SetActive(false);
    }

    
    public void ShowLevelWinUI()
    {
        if (levelWinScreen != null) levelWinScreen.SetActive(true);
        UpdateWinScreen();
    }

    public void HideLevelWinUI()
    {
        if (levelWinScreen != null) levelWinScreen.SetActive(false);
    }

    
    public void ShowGameOverUI()
    {
        if (gameOverScreen != null) gameOverScreen.SetActive(true);
    }

    public void HideGameOverUI()
    {
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
    }

    
    private void UpdateTimerDisplay()
    {
        if (timerText != null && LevelManager.Instance != null)
        {
            float time = LevelManager.Instance.GetRemainingTime();
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    private void UpdateLevelNumber()
    {
        if (levelNumberText != null && LevelManager.Instance != null)
        {
            int levelNum = LevelManager.Instance.GetCurrentLevelIndex() + 1;
            levelNumberText.text = $"Level {levelNum}";
        }
    }

    private void UpdateWinScreen()
    {
        if (winTimeText != null && LevelManager.Instance != null)
        {
            float time = LevelManager.Instance.GetRemainingTime();
            winTimeText.text = $"Time Remaining: {time:F2}s";
        }
    }

    
    public void OnStartButtonClicked()
    {
        GameStateManager.Instance?.StartGameplay();
    }

    public void OnPauseButtonClicked()
    {
        GameStateManager.Instance?.PauseGame();
    }

    public void OnResumeButtonClicked()
    {
        GameStateManager.Instance?.GetPauseState()?.ResumeGame();
    }

    public void OnRestartButtonClicked()
    {
        LevelManager.Instance?.RestartLevel();
        GameStateManager.Instance?.StartGameplay();
    }

    public void OnNextLevelButtonClicked()
    {
        LevelManager.Instance?.LoadNextLevel();
        GameStateManager.Instance?.StartGameplay();
    }

    public void OnQuitToMenuButtonClicked()
    {
        GameStateManager.Instance?.GoToMenu();
    }
}
