using UnityEngine;

[CreateAssetMenu(fileName = "New Obstacle", menuName = "Game/Obstacles/Obstacle Behavior")]
public class ObstacleBehaviorSO : ScriptableObject
{
    [Header("Obstacle Info")]
    [SerializeField] private string obstacleName;
    [SerializeField] private ObstacleType obstacleType;
    [SerializeField, TextArea] private string description;

    [Header("Damage")]
    [SerializeField] private bool killsPlayer = true;
    [SerializeField] private int damageAmount = 1;

    [Header("Movement (For Moving Obstacles)")]
    [SerializeField] private bool isMoving = false;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveDistance = 5f;
    [SerializeField] private MovementPattern movementPattern;

    [Header("Falling Platform Settings")]
    [SerializeField] private bool isFallingPlatform = false;
    [SerializeField] private float fallDelay = 0.5f;
    [SerializeField] private float respawnDelay = 3f;

    [Header("Boulder Settings")]
    [SerializeField] private bool isBoulder = false;
    [SerializeField] private float boulderMass = 10f;
    [SerializeField] private bool canBeStoppedByTelekinesis = true;

    [Header("Visual")]
    [SerializeField] private Color obstacleColor = Color.red;
    [SerializeField] private bool pulseWarning = false;
    [SerializeField] private float pulseSpeed = 1f;

    
    public string ObstacleName => obstacleName;
    public ObstacleType Type => obstacleType;
    public string Description => description;
    public bool KillsPlayer => killsPlayer;
    public int DamageAmount => damageAmount;
    public bool IsMoving => isMoving;
    public float MoveSpeed => moveSpeed;
    public float MoveDistance => moveDistance;
    public MovementPattern Pattern => movementPattern;
    public bool IsFallingPlatform => isFallingPlatform;
    public float FallDelay => fallDelay;
    public float RespawnDelay => respawnDelay;
    public bool IsBoulder => isBoulder;
    public float BoulderMass => boulderMass;
    public bool CanBeStoppedByTelekinesis => canBeStoppedByTelekinesis;
    public Color ObstacleColor => obstacleColor;
    public bool PulseWarning => pulseWarning;
    public float PulseSpeed => pulseSpeed;

    private void OnValidate()
    {
        moveSpeed = Mathf.Max(0f, moveSpeed);
        moveDistance = Mathf.Max(0f, moveDistance);
        damageAmount = Mathf.Max(0, damageAmount);
    }
}

public enum ObstacleType
{
    Spike,
    Boulder,
    FallingPlatform,
    MovingSurface,
    Lava,
    Saw,
    Crusher,
    Other
}

public enum MovementPattern
{
    Horizontal,
    Vertical,
    Circular,
    PingPong,
    Random
}
