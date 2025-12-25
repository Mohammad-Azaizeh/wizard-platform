using UnityEngine;

public class ExitGate : MonoBehaviour
{
    [Header("Visual Settings")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private bool animateGate = true;
    [SerializeField] private float pulseSpeed = 2f;
    [SerializeField] private Color gateColor = Color.green;

    [Header("Activation")]
    [SerializeField] private bool requiresAllObjectives = false;
    [SerializeField] private int objectivesRequired = 0;
    private int currentObjectives = 0;
    private bool isActive = true;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.color = gateColor;
        }
    }

    private void Update()
    {
        if (animateGate && isActive)
        {
            AnimateGate();
        }
    }

    private void AnimateGate()
    {
        if (spriteRenderer == null) return;

        float pulse = Mathf.PingPong(Time.time * pulseSpeed, 1f);
        Color color = gateColor;
        color.a = Mathf.Lerp(0.6f, 1f, pulse);
        spriteRenderer.color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive) return;

        if (collision.CompareTag("Player"))
        {
            if (CanComplete())
            {
                CompleteLevel();
            }
        }
    }

    private bool CanComplete()
    {
        if (!requiresAllObjectives) return true;
        return currentObjectives >= objectivesRequired;
    }

    private void CompleteLevel()
    {
        Debug.Log("Player reached exit gate!");
        LevelManager.Instance?.OnLevelComplete();
    }

    public void SetActive(bool active)
    {
        isActive = active;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = active ? gateColor : Color.gray;
        }
    }
}
