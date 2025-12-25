using UnityEngine;

public class SpikeObstacle : Obstacle
{
    protected override void Initialize()
    {
        Debug.Log($"Spike obstacle initialized at {transform.position}");
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
