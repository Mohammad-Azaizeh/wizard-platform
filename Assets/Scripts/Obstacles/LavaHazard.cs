using UnityEngine;

public class LavaHazard : Obstacle
{
    [Header("Lava Settings")]
    [SerializeField] private bool animateSurface = true;
    [SerializeField] private float waveSpeed = 1f;
    [SerializeField] private float waveAmplitude = 0.1f;

    private Vector3 originalScale;

    protected override void Awake()
    {
        base.Awake();
        originalScale = transform.localScale;
    }

    protected override void Initialize()
    {
    }

    protected override void Update()
    {
        base.Update();

        if (animateSurface)
        {
            AnimateLavaSurface();
        }
    }

    private void AnimateLavaSurface()
    {
        float wave = Mathf.Sin(Time.time * waveSpeed) * waveAmplitude;
        transform.localScale = originalScale + new Vector3(0f, wave, 0f);
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
