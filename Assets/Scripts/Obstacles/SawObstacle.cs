using UnityEngine;

public class SawObstacle : Obstacle
{
    [Header("Saw Settings")]
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private bool rotateClockwise = true;

    protected override void Initialize()
    {
    }

    protected override void Update()
    {
        base.Update();

        if (isActive)
        {
            RotateSaw();
        }
    }

    private void RotateSaw()
    {
        float rotation = rotationSpeed * Time.deltaTime;
        if (!rotateClockwise)
            rotation = -rotation;

        transform.Rotate(0f, 0f, rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            OnPlayerCollision(player);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            OnPlayerCollision(player);
        }
    }
}
