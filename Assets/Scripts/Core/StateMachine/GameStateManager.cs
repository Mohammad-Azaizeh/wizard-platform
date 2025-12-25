using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;


public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    private GameState currentState;

    
    private MenuState menuState;
    private GameplayState gameplayState;
    private PauseState pauseState;
    private LevelWinState levelWinState;
    private GameOverState gameOverState;

    [Header("References")]
    [SerializeField] private UIManager uiManager;

    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeStates();
    }

    private void Start()
    {
        
        ChangeState(menuState);
    }

    private void Update()
    {
        currentState?.Update();
    }

    private void InitializeStates()
    {
        menuState = new MenuState(this);
        gameplayState = new GameplayState(this);
        pauseState = new PauseState(this);
        levelWinState = new LevelWinState(this);
        gameOverState = new GameOverState(this);
    }

    public void ChangeState(GameState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    
    public void GoToMenu() => ChangeState(menuState);
    public void StartGameplay() => ChangeState(gameplayState);
    public void PauseGame() => ChangeState(pauseState);
    public void WinLevel() => ChangeState(levelWinState);
    public void GameOver() => ChangeState(gameOverState);

   
    public GameplayState GetGameplayState() => gameplayState;
    public PauseState GetPauseState() => pauseState;

    
    public UIManager GetUIManager() => uiManager;
}
