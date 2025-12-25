using UnityEngine;
using UnityEngine.InputSystem;

public class TelekinesisManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private TelekinesisAbilitySO abilityData;

    [Header("References")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Camera mainCamera;

    [Header("Visual Effects")]
    [SerializeField] private LineRenderer telekinesisLine;
    [SerializeField] private ParticleSystem pullEffect;
    [SerializeField] private ParticleSystem pushEffect;
    [SerializeField] private ParticleSystem holdEffect;

    [Header("Events")]
    [SerializeField] private GameEventSO telekinesisUsedEvent;

    private TelekinesisState currentState;
    private TelekinesisIdleState idleState;
    private TelekinesisPullingState pullingState;
    private TelekinesisPushingState pushingState;
    private TelekinesisHoldingState holdingState;

    private ITelekinesisTarget currentTarget;
    private ITelekinesisTarget hoveredTarget;

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (playerTransform == null)
            playerTransform = transform;

        InitializeStates();
    }

    private void Start()
    {
        ChangeState(idleState);
    }

    private void InitializeStates()
    {
        idleState = new TelekinesisIdleState(this);
        pullingState = new TelekinesisPullingState(this);
        pushingState = new TelekinesisPushingState(this);
        holdingState = new TelekinesisHoldingState(this);
    }

    private void Update()
    {
        
        currentState?.Update();
    }

    private void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }

    public void ChangeState(TelekinesisState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public void UpdateTargetScanning()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        
       

        RaycastHit2D hit = Physics2D.Raycast(
            worldPosition,
            Vector2.zero,
            0f,
            abilityData.TargetLayers
        );

        
        

        ITelekinesisTarget newTarget = null;

        if (hit.collider != null)
        {
           

            newTarget = hit.collider.GetComponent<ITelekinesisTarget>();


            if (newTarget != null && newTarget.CanBeAffected())
            {
                float distance = Vector2.Distance(playerTransform.position, hit.point);
                

                if (distance > abilityData.MaxRange)
                {
                   
                    newTarget = null;
                }
            }
        }

        if (newTarget != hoveredTarget)
        {
            hoveredTarget?.OnUntargeted();
            hoveredTarget = newTarget;
            hoveredTarget?.OnTargeted();
        }

        currentTarget = hoveredTarget;
        UpdateTelekinesisLine();
    }
    private void UpdateTelekinesisLine()
    {
        if (telekinesisLine == null) return;

        if (currentTarget != null && currentState != idleState)
        {
            telekinesisLine.enabled = true;
            telekinesisLine.SetPosition(0, playerTransform.position);
            telekinesisLine.SetPosition(1, currentTarget.GetGameObject().transform.position);
        }
        else
        {
            telekinesisLine.enabled = false;
        }
    }

    public void ApplyPullForce()
    {
        if (currentTarget == null) return;

        Vector2 targetPos = currentTarget.GetGameObject().transform.position;
        Vector2 playerPos = playerTransform.position;
        Vector2 direction = (playerPos - targetPos).normalized;

        currentTarget.ApplyPullForce(direction, abilityData.PullForce);
        telekinesisUsedEvent?.Raise();
    }

    public void ApplyPushForce()
    {
        if (currentTarget == null) return;

        Vector2 targetPos = currentTarget.GetGameObject().transform.position;
        Vector2 playerPos = playerTransform.position;
        Vector2 direction = (targetPos - playerPos).normalized;

        currentTarget.ApplyPushForce(direction, abilityData.PushForce);
        telekinesisUsedEvent?.Raise();
    }

    public void HoldObjectAtPosition()
    {
        if (currentTarget == null) return;

        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        Vector2 directionToMouse = (worldPosition - (Vector2)playerTransform.position).normalized;
        Vector2 targetHoldPosition = (Vector2)playerTransform.position + directionToMouse * abilityData.HoldDistance;

        currentTarget.HoldAtPosition(targetHoldPosition, abilityData.HoldSmoothing);
        UpdateTelekinesisLine();
    }

    public void GrabCurrentTarget()
    {
        if (currentTarget == null) return;
        currentTarget.OnGrabbed();
    }

    public void ReleaseCurrentTarget()
    {
        if (currentTarget == null) return;
        currentTarget.OnReleased();
    }

   

    public void ShowPullEffect()
    {
        if (pullEffect != null && !pullEffect.isPlaying)
            pullEffect.Play();
    }

    public void HidePullEffect()
    {
        if (pullEffect != null && pullEffect.isPlaying)
            pullEffect.Stop();
    }

    public void ShowPushEffect()
    {
        if (pushEffect != null && !pushEffect.isPlaying)
            pushEffect.Play();
    }

    public void HidePushEffect()
    {
        if (pushEffect != null && pushEffect.isPlaying)
            pushEffect.Stop();
    }

    public void ShowHoldEffect()
    {
        if (holdEffect != null && !holdEffect.isPlaying)
            holdEffect.Play();
    }

    public void HideHoldEffect()
    {
        if (holdEffect != null && holdEffect.isPlaying)
            holdEffect.Stop();
    }

    public TelekinesisIdleState GetIdleState() => idleState;
    public TelekinesisPullingState GetPullingState() => pullingState;
    public TelekinesisPushingState GetPushingState() => pushingState;
    public TelekinesisHoldingState GetHoldingState() => holdingState;

    public bool HasCurrentTarget() => currentTarget != null && currentTarget.CanBeAffected();

    public bool IsTargetInRange()
    {
        if (currentTarget == null) return false;

        float distance = Vector2.Distance(
            playerTransform.position,
            currentTarget.GetGameObject().transform.position
        );

        return distance >= abilityData.MinRange && distance <= abilityData.MaxRange;
    }

    public TelekinesisAbilitySO GetAbilityData() => abilityData;
}
