using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] protected ObstacleBehaviorSO obstacleData;

    [Header("Visual")]
    [SerializeField] protected SpriteRenderer spriteRenderer;

    protected bool isActive = true;

    protected virtual void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        ApplyVisualSettings();
    }

    protected virtual void Start()
    {
        Initialize();
    }

    protected virtual void Update()
    {
        if (isActive && obstacleData != null && obstacleData.PulseWarning)
        {
            UpdatePulseEffect();
        }
    }

    protected abstract void Initialize();

    public virtual void OnPlayerCollision(PlayerHealth player)
    {
        if (!isActive || player == null) return;

        if (obstacleData.KillsPlayer)
        {
            player.TakeDamage(9999);
        }
        else
        {
            player.TakeDamage(obstacleData.DamageAmount);
        }
    }

    protected void ApplyVisualSettings()
    {
        if (spriteRenderer != null && obstacleData != null)
        {
            spriteRenderer.color = obstacleData.ObstacleColor;
        }
    }

    protected void UpdatePulseEffect()
    {
        if (spriteRenderer == null || obstacleData == null) return;

        float pulse = Mathf.PingPong(Time.time * obstacleData.PulseSpeed, 1f);
        Color color = obstacleData.ObstacleColor;
        color.a = Mathf.Lerp(0.5f, 1f, pulse);
        spriteRenderer.color = color;
    }

    public virtual void SetActive(bool active)
    {
        isActive = active;
    }

    public bool IsActive() => isActive;
    public ObstacleBehaviorSO GetObstacleData() => obstacleData;
}