using UnityEngine;

public class MovingObstacle : Obstacle
{
    private Vector3 startPosition;
    private float moveTimer = 0f;
    private Vector3 moveDirection = Vector3.right;
    private bool movingForward = true;

    protected override void Initialize()
    {
        startPosition = transform.position;

        if (obstacleData == null || !obstacleData.IsMoving) return;

        switch (obstacleData.Pattern)
        {
            case MovementPattern.Horizontal:
                moveDirection = Vector3.right;
                break;
            case MovementPattern.Vertical:
                moveDirection = Vector3.up;
                break;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (!isActive || obstacleData == null || !obstacleData.IsMoving) return;

        UpdateMovement();
    }

    private void UpdateMovement()
    {
        moveTimer += Time.deltaTime * obstacleData.MoveSpeed;

        switch (obstacleData.Pattern)
        {
            case MovementPattern.Horizontal:
                MoveLinear(Vector3.right);
                break;

            case MovementPattern.Vertical:
                MoveLinear(Vector3.up);
                break;

            case MovementPattern.Circular:
                MoveCircular();
                break;

            case MovementPattern.PingPong:
                MovePingPong();
                break;

            case MovementPattern.Random:
                MoveRandom();
                break;
        }
    }

    private void MoveLinear(Vector3 direction)
    {
        float offset = Mathf.Sin(moveTimer) * obstacleData.MoveDistance;
        transform.position = startPosition + direction * offset;
    }

    private void MoveCircular()
    {
        float x = Mathf.Cos(moveTimer) * obstacleData.MoveDistance;
        float y = Mathf.Sin(moveTimer) * obstacleData.MoveDistance;
        transform.position = startPosition + new Vector3(x, y, 0f);
    }

    private void MovePingPong()
    {
        float distance = moveTimer * obstacleData.MoveSpeed;

        if (distance >= obstacleData.MoveDistance && movingForward)
        {
            movingForward = false;
            moveTimer = 0f;
        }
        else if (distance >= obstacleData.MoveDistance && !movingForward)
        {
            movingForward = true;
            moveTimer = 0f;
        }

        Vector3 offset = moveDirection * distance * (movingForward ? 1 : -1);
        transform.position = startPosition + offset;
    }

    private void MoveRandom()
    {
        float x = Mathf.PerlinNoise(moveTimer * 0.5f, 0f) * obstacleData.MoveDistance * 2f - obstacleData.MoveDistance;
        float y = Mathf.PerlinNoise(0f, moveTimer * 0.5f) * obstacleData.MoveDistance * 2f - obstacleData.MoveDistance;
        transform.position = startPosition + new Vector3(x, y, 0f);
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !obstacleData.KillsPlayer)
        {
            Vector2 contactNormal = collision.contacts[0].normal;
            if (contactNormal.y < -0.5f)
            {
                collision.transform.SetParent(transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
