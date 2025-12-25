using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Game/Level Data")]
public class LevelDataSO : ScriptableObject
{
    [Header("Level Info")]
    [SerializeField] private string levelName;
    [SerializeField, TextArea] private string levelDescription;
    [SerializeField] private int levelNumber;

    [Header("Time")]
    [SerializeField] private float timeLimit = 180f;

    [Header("Level Prefab")]
    [SerializeField] private GameObject levelPrefab;

    [Header("Spawn Points")]
    [SerializeField] private Vector3 playerSpawnPosition;
    [SerializeField] private Vector3 exitGatePosition;

    [Header("Obstacles (Optional)")]
    [SerializeField] private ObstacleBehaviorSO[] obstacles;

    
    public string LevelName => levelName;
    public string LevelDescription => levelDescription;
    public int LevelNumber => levelNumber;
    public float TimeLimit => timeLimit;
    public GameObject LevelPrefab => levelPrefab;
    public Vector3 PlayerSpawnPosition => playerSpawnPosition;
    public Vector3 ExitGatePosition => exitGatePosition;
    public ObstacleBehaviorSO[] Obstacles => obstacles;

    private void OnValidate()
    {
        if (timeLimit <= 0f)
        {
            timeLimit = 60f;
        }
    }
}
