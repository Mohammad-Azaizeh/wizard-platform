using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private PlayerStatsSO playerStats;

    [Header("Events")]
    [SerializeField] private GameEventSO playerDamagedEvent;

    [Header("References")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    
    private int currentHealth;
    private bool isInvincible;
    private float invincibilityTimer;
    private bool isDead;

    
    private int deathHash;

    private void Awake()
    {
        if (playerController == null)
            playerController = GetComponent<PlayerController>();

        if (animator == null)
            animator = GetComponent<Animator>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (playerStats != null && animator != null)
        {
            deathHash = Animator.StringToHash(playerStats.DeathTrigger);
        }
    }

    private void Start()
    {
        currentHealth = playerStats != null ? playerStats.MaxHealth : 1;
        isInvincible = false;
        isDead = false;
    }

    private void Update()
    {
        if (isInvincible)
        {
            UpdateInvincibility();
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible || isDead) return;

        currentHealth -= damage;
        Debug.Log($"Player took {damage} damage. Health: {currentHealth}");

        
        playerDamagedEvent?.Raise();

        if (currentHealth <= 0)
        {
            Die();
        }
        else if (playerStats != null && playerStats.HasInvincibilityFrames)
        {
            StartInvincibility();
        }
    }

    private void StartInvincibility()
    {
        isInvincible = true;
        invincibilityTimer = playerStats.InvincibilityDuration;
    }

    private void UpdateInvincibility()
    {
        invincibilityTimer -= Time.deltaTime;

        if (invincibilityTimer <= 0f)
        {
            isInvincible = false;

            
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = 1f;
                spriteRenderer.color = color;
            }
        }
        else
        {
            
            if (spriteRenderer != null)
            {
                float alpha = Mathf.PingPong(Time.time * 10f, 0.5f) + 0.5f;
                Color color = spriteRenderer.color;
                color.a = alpha;
                spriteRenderer.color = color;
            }
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Player died!");

        
        if (animator != null)
            animator.SetTrigger(deathHash);

        
        if (playerController != null)
            playerController.enabled = false;

        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        
        LevelManager.Instance?.OnPlayerDied();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            obstacle.OnPlayerCollision(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("ExitGate"))
        {
            LevelManager.Instance?.OnLevelComplete();
        }

        
        Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            obstacle.OnPlayerCollision(this);
        }
    }

    
    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => playerStats != null ? playerStats.MaxHealth : 1;
    public bool IsInvincible() => isInvincible;
    public bool IsDead() => isDead;

    
    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth = Mathf.Min(currentHealth + amount, playerStats.MaxHealth);
        Debug.Log($"Player healed {amount}. Health: {currentHealth}");
    }

    public void SetInvincible(float duration)
    {
        isInvincible = true;
        invincibilityTimer = duration;
    }
}
