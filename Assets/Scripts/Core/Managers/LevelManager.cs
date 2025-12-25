using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("Level Data")]
    [SerializeField] private LevelDataSO[] allLevels;
    private int currentLevelIndex = 0;
    private LevelDataSO currentLevel;

    [Header("Timer")]
    private float remainingTime;
    private bool timerRunning = false;

    [Header("Game Events")]
    [SerializeField] private GameEventSO levelCompletedEvent;
    [SerializeField] private GameEventSO timeExpiredEvent;
    [SerializeField] private GameEventSO playerDiedEvent;

    [Header("Level Objects")]
    private GameObject currentLevelInstance;
    private Transform playerSpawnPoint;
    private Transform exitGate;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (timerRunning)
        {
            UpdateTimer();
        }
    }

    public void StartLevel()
    {
        if (currentLevel == null && allLevels.Length > 0)
        {
            LoadLevel(0);
        }
        else if (currentLevel != null)
        {
            InitializeLevel();
        }
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= allLevels.Length)
        {
            Debug.LogWarning($"Level index {levelIndex} out of range!");
            return;
        }

        currentLevelIndex = levelIndex;
        currentLevel = allLevels[levelIndex];

        if (currentLevelInstance != null)
        {
            Destroy(currentLevelInstance);
        }

        
        if (currentLevel.LevelPrefab != null)
        {
            currentLevelInstance = Instantiate(currentLevel.LevelPrefab);
            FindLevelObjects();
        }

        InitializeLevel();
    }

    private void InitializeLevel()
    {
        
        remainingTime = currentLevel.TimeLimit;
        timerRunning = true;

        PositionPlayer();

        Debug.Log($"Level {currentLevelIndex + 1} started! Time limit: {remainingTime}s");
    }

    private void FindLevelObjects()
    {
        
        GameObject spawnObj = GameObject.FindGameObjectWithTag("PlayerSpawn");
        GameObject exitObj = GameObject.FindGameObjectWithTag("ExitGate");

        if (spawnObj != null) playerSpawnPoint = spawnObj.transform;
        if (exitObj != null) exitGate = exitObj.transform;
    }

    private void PositionPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && playerSpawnPoint != null)
        {
            player.transform.position = playerSpawnPoint.position;
        }
    }

    private void UpdateTimer()
    {
        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            timerRunning = false;
            OnTimeExpired();
        }
    }

    public void OnLevelComplete()
    {
        timerRunning = false;
        Debug.Log("Level Complete!");

        levelCompletedEvent?.Raise();
        GameStateManager.Instance?.WinLevel();
    }

    private void OnTimeExpired()
    {
        Debug.Log("Time Expired!");

        timeExpiredEvent?.Raise();
        GameStateManager.Instance?.GameOver();
    }

    public void OnPlayerDied()
    {
        timerRunning = false;
        Debug.Log("Player Died!");

        playerDiedEvent?.Raise();
        GameStateManager.Instance?.GameOver();
    }

    public void LoadNextLevel()
    {
        int nextLevel = currentLevelIndex + 1;
        if (nextLevel < allLevels.Length)
        {
            LoadLevel(nextLevel);
        }
        else
        {
            Debug.Log("No more levels! Returning to menu.");
            GameStateManager.Instance?.GoToMenu();
        }
    }

    public void RestartLevel()
    {
        LoadLevel(currentLevelIndex);
    }

    
    public float GetRemainingTime() => remainingTime;
    public int GetCurrentLevelIndex() => currentLevelIndex;
    public LevelDataSO GetCurrentLevel() => currentLevel;
}
