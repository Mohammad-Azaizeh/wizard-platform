using UnityEngine;


[CreateAssetMenu(fileName = "New Player Stats", menuName = "Game/Player Stats")]
public class PlayerStatsSO : ScriptableObject
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float deceleration = 50f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float gravityScale = 3f;
    [SerializeField] private float fallGravityMultiplier = 1.5f;
    [SerializeField] private float maxFallSpeed = 20f;

    [Header("Air Control")]
    [SerializeField, Range(0f, 1f)] private float airControl = 0.8f;

    [Header("Ground Check")]
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayers;

    [Header("Health")]
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private bool hasInvincibilityFrames = true;
    [SerializeField] private float invincibilityDuration = 1f;

    [Header("Animation Parameters")]
    [SerializeField] private string moveSpeedParam = "MoveSpeed";
    [SerializeField] private string isGroundedParam = "IsGrounded";
    [SerializeField] private string jumpTrigger = "Jump";
    [SerializeField] private string deathTrigger = "Death";

    
    public float MoveSpeed => moveSpeed;
    public float Acceleration => acceleration;
    public float Deceleration => deceleration;
    public float JumpForce => jumpForce;
    public float GravityScale => gravityScale;
    public float FallGravityMultiplier => fallGravityMultiplier;
    public float MaxFallSpeed => maxFallSpeed;
    public float AirControl => airControl;
    public float GroundCheckDistance => groundCheckDistance;
    public LayerMask GroundLayers => groundLayers;
    public int MaxHealth => maxHealth;
    public bool HasInvincibilityFrames => hasInvincibilityFrames;
    public float InvincibilityDuration => invincibilityDuration;
    public string MoveSpeedParam => moveSpeedParam;
    public string IsGroundedParam => isGroundedParam;
    public string JumpTrigger => jumpTrigger;
    public string DeathTrigger => deathTrigger;

    private void OnValidate()
    {
        moveSpeed = Mathf.Max(0f, moveSpeed);
        jumpForce = Mathf.Max(0f, jumpForce);
        gravityScale = Mathf.Max(0f, gravityScale);
        maxHealth = Mathf.Max(1, maxHealth);
    }
}