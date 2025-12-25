using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private PlayerStatsSO playerStats;

    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private SpriteRenderer spriteRenderer;

    
    private Rigidbody2D rb;

    
    private Vector2 moveInput;
    private float currentSpeed;
    private bool facingRight = true;

    
    private bool isGrounded;
    private bool jumpRequested;

    
    private int currentHealth;
    private bool isInvincible;
    private float invincibilityTimer;

    
    private int moveSpeedHash;
    private int isGroundedHash;
    private int jumpHash;
    private int deathHash;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (inputReader == null)
            inputReader = GameObject.FindObjectOfType<InputReader>();

        if (animator == null)
            animator = GetComponent<Animator>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        
        if (animator != null && playerStats != null)
        {
            moveSpeedHash = Animator.StringToHash(playerStats.MoveSpeedParam);
            isGroundedHash = Animator.StringToHash(playerStats.IsGroundedParam);
            jumpHash = Animator.StringToHash(playerStats.JumpTrigger);
            deathHash = Animator.StringToHash(playerStats.DeathTrigger);
        }
    }

    private void OnEnable()
    {
        if (inputReader != null)
        {
            inputReader.MovePerformed += OnMoveInput;
            inputReader.JumpPerformed += OnJumpInput;
            inputReader.PausePerformed += OnPauseInput;
        }

        currentHealth = playerStats != null ? playerStats.MaxHealth : 1;
        isInvincible = false;
    }

    private void OnDisable()
    {
        if (inputReader != null)
        {
            inputReader.MovePerformed -= OnMoveInput;
            inputReader.JumpPerformed -= OnJumpInput;
            inputReader.PausePerformed -= OnPauseInput;
        }
    }

    private void Update()
    {
        CheckGrounded();
        UpdateAnimations();
        UpdateInvincibility();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
        ApplyGravityModifiers();
    }

    private void OnMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    private void OnJumpInput()
    {
        if (isGrounded)
        {
            jumpRequested = true;
        }
    }

    private void OnPauseInput()
    {
        GameStateManager.Instance?.PauseGame();
    }

    private void HandleMovement()
    {
        if (playerStats == null) return;

        float targetSpeed = moveInput.x * playerStats.MoveSpeed;
        float acceleration = isGrounded ? playerStats.Acceleration : playerStats.Acceleration * playerStats.AirControl;

        
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.fixedDeltaTime);

        
        rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y);

        
        if (moveInput.x > 0 && !facingRight)
            Flip();
        else if (moveInput.x < 0 && facingRight)
            Flip();
    }

    private void HandleJump()
    {
        if (jumpRequested && isGrounded && playerStats != null)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, playerStats.JumpForce);
            jumpRequested = false;

            
            if (animator != null)
                animator.SetTrigger(jumpHash);
        }
    }

    private void ApplyGravityModifiers()
    {
        if (playerStats == null) return;

        
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = playerStats.GravityScale * playerStats.FallGravityMultiplier;

            
            if (rb.linearVelocity.y < -playerStats.MaxFallSpeed)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -playerStats.MaxFallSpeed);
            }
        }
        else
        {
            rb.gravityScale = playerStats.GravityScale;
        }
    }

    private void CheckGrounded()
    {
        if (playerStats == null) return;

        Vector2 checkPosition = groundCheck != null ? groundCheck.position : (Vector2)transform.position;

        isGrounded = Physics2D.Raycast(
            checkPosition,
            Vector2.down,
            playerStats.GroundCheckDistance,
            playerStats.GroundLayers
        );

        
        Debug.DrawRay(checkPosition, Vector2.down * playerStats.GroundCheckDistance,
            isGrounded ? Color.green : Color.red);
    }

    private void Flip()
    {
        facingRight = !facingRight;

        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !facingRight;
        }
        else
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void UpdateAnimations()
    {
        if (animator == null) return;

        
        animator.SetFloat(moveSpeedHash, Mathf.Abs(currentSpeed));
        animator.SetBool(isGroundedHash, isGrounded);
    }

    private void UpdateInvincibility()
    {
        if (!isInvincible) return;

        invincibilityTimer -= Time.deltaTime;

        if (invincibilityTimer <= 0f)
        {
            isInvincible = false;

            
            if (spriteRenderer != null)
                spriteRenderer.color = Color.white;
        }
        else
        {
            
            if (spriteRenderer != null)
            {
                float alpha = Mathf.PingPong(Time.time * 10f, 1f);
                spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        else if (playerStats != null && playerStats.HasInvincibilityFrames)
        {
            isInvincible = true;
            invincibilityTimer = playerStats.InvincibilityDuration;
        }
    }

    private void Die()
    {
        
        if (animator != null)
            animator.SetTrigger(deathHash);

        
        enabled = false;
        rb.linearVelocity = Vector2.zero;

        
        LevelManager.Instance?.OnPlayerDied();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("ExitGate"))
        {
            LevelManager.Instance?.OnLevelComplete();
        }
    }

    
    public bool IsGrounded() => isGrounded;
    public Vector2 GetVelocity() => rb.linearVelocity;
    public bool IsFacingRight() => facingRight;
}
