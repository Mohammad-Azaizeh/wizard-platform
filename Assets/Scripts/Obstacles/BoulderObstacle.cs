using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BoulderObstacle : Obstacle, ITelekinesisTarget
{
    [Header("Boulder Settings")]
    [SerializeField] private TelekinesisObjectSO telekinesisData;
    [SerializeField] private Vector2 initialVelocity = new Vector2(-2f, 0f);
    [SerializeField] private bool startMoving = true;

    private Rigidbody2D rb;
    private bool isHeld = false;
    private Color originalColor;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    protected override void Initialize()
    {
        if (obstacleData != null && obstacleData.IsBoulder)
        {
            rb.mass = obstacleData.BoulderMass;
        }

        if (startMoving)
        {
            rb.linearVelocity = initialVelocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            OnPlayerCollision(player);
        }
    }

    public void OnTargeted()
    {
        if (obstacleData != null && !obstacleData.CanBeStoppedByTelekinesis) return;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.Lerp(originalColor, Color.cyan, 0.5f);
        }
    }

    public void OnUntargeted()
    {
        if (!isHeld && spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }

    public void OnGrabbed()
    {
        if (obstacleData != null && !obstacleData.CanBeStoppedByTelekinesis) return;

        isHeld = true;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.yellow;
        }

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    public void OnReleased()
    {
        isHeld = false;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }

    public void ApplyPullForce(Vector2 direction, float force)
    {
        if (obstacleData != null && !obstacleData.CanBeStoppedByTelekinesis) return;
        rb.AddForce(direction * force * 0.5f);
    }

    public void ApplyPushForce(Vector2 direction, float force)
    {
        if (obstacleData != null && !obstacleData.CanBeStoppedByTelekinesis) return;
        rb.AddForce(direction * force * 0.5f);
    }

    public void HoldAtPosition(Vector2 targetPosition, float smoothing)
    {
        if (obstacleData != null && !obstacleData.CanBeStoppedByTelekinesis) return;

        Vector2 currentPos = rb.position;
        Vector2 newPos = Vector2.Lerp(currentPos, targetPosition, smoothing * 0.3f * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    public void Rotate(float angle)
    {
        rb.MoveRotation(rb.rotation + angle);
    }

    public bool CanBeAffected()
    {
        return obstacleData != null && obstacleData.CanBeStoppedByTelekinesis;
    }

    public GameObject GetGameObject() => gameObject;
    public Rigidbody2D GetRigidbody() => rb;
    public TelekinesisObjectSO GetObjectData() => telekinesisData;
}
