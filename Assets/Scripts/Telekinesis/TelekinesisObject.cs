using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TelekinesisObject : MonoBehaviour, ITelekinesisTarget
{
    [Header("Configuration")]
    [SerializeField] private TelekinesisObjectSO objectData;

    [Header("Visual Feedback")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private Color originalColor;
    private bool isTargeted;
    private bool isGrabbed;
    private bool wasKinematic;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;

        
        if (objectData != null)
        {
            rb.mass = objectData.Mass;
            rb.linearDamping = objectData.Drag;
            rb.angularDamping = objectData.AngularDrag;
        }
    }

    public void OnTargeted()
    {
        if (objectData != null && !objectData.ShowHighlight) return;

        isTargeted = true;

        
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.Lerp(originalColor, Color.cyan, 0.5f);
        }
    }

    public void OnUntargeted()
    {
        isTargeted = false;

        if (!isGrabbed && spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }

    public void OnGrabbed()
    {
        isGrabbed = true;

        
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.yellow;
        }

        
        wasKinematic = rb.bodyType == RigidbodyType2D.Kinematic;
        rb.bodyType = RigidbodyType2D.Dynamic;

        
        if (objectData != null && objectData.FreezeRotationWhenHeld)
        {
            rb.freezeRotation = true;
        }
    }

    public void OnReleased()
    {
        isGrabbed = false;

        
        if (spriteRenderer != null)
        {
            spriteRenderer.color = isTargeted ? Color.Lerp(originalColor, Color.cyan, 0.5f) : originalColor;
        }

        
        rb.bodyType = wasKinematic ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;

        
        if (objectData != null && objectData.FreezeRotationWhenHeld)
        {
            rb.freezeRotation = false;
        }
    }

    public void ApplyPullForce(Vector2 direction, float force)
    {
        if (objectData != null && !objectData.CanBePulled) return;

        float finalForce = force * (objectData != null ? objectData.ForceMultiplier : 1f);
        rb.AddForce(direction * finalForce);
    }

    public void ApplyPushForce(Vector2 direction, float force)
    {
        if (objectData != null && !objectData.CanBePushed) return;

        float finalForce = force * (objectData != null ? objectData.ForceMultiplier : 1f);
        rb.AddForce(direction * finalForce);
    }

    public void HoldAtPosition(Vector2 targetPosition, float smoothing)
    {
        if (objectData != null && !objectData.CanBeHeld) return;

        Vector2 currentPos = rb.position;
        Vector2 newPos = Vector2.Lerp(currentPos, targetPosition, smoothing * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    public void Rotate(float angle)
    {
        if (objectData != null && !objectData.CanBeRotated) return;

        rb.MoveRotation(rb.rotation + angle);
    }

    public bool CanBeAffected()
    {
        return objectData != null &&
               (objectData.CanBePulled || objectData.CanBePushed || objectData.CanBeHeld);
    }

    public GameObject GetGameObject() => gameObject;
    public Rigidbody2D GetRigidbody() => rb;
    public TelekinesisObjectSO GetObjectData() => objectData;
}