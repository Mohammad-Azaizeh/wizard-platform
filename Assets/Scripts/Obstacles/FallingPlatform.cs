using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallingPlatform : Obstacle
{
    private Rigidbody2D rb;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isFalling = false;
    private bool hasPlayerOnTop = false;
    private float fallTimer = 0f;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Initialize()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 1f;
    }

    protected override void Update()
    {
        base.Update();

        if (hasPlayerOnTop && !isFalling)
        {
            fallTimer += Time.deltaTime;

            if (fallTimer >= obstacleData.FallDelay)
            {
                StartFalling();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 contactNormal = collision.contacts[0].normal;
            if (contactNormal.y < -0.5f)
            {
                hasPlayerOnTop = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasPlayerOnTop = false;
        }
    }

    private void StartFalling()
    {
        if (isFalling) return;

        isFalling = true;
        rb.bodyType = RigidbodyType2D.Dynamic;

        StartCoroutine(RespawnAfterDelay());
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(obstacleData.RespawnDelay);

        isFalling = false;
        hasPlayerOnTop = false;
        fallTimer = 0f;

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }

    public override void OnPlayerCollision(PlayerHealth player)
    {
    }
}
