using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(TelekinesisManager))]
public class TelekinesisController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private TelekinesisManager telekinesisManager;

    private void Awake()
    {
        if (inputReader == null)
            inputReader = GameObject.FindObjectOfType<InputReader>();
        if (telekinesisManager == null)
            telekinesisManager = GetComponent<TelekinesisManager>();
    }

    private void OnEnable()
    {
        if (inputReader != null)
        {
            inputReader.PullStarted += OnPullStarted;
            inputReader.PullCanceled += OnPullCanceled;
            inputReader.PushStarted += OnPushStarted;
            inputReader.PushCanceled += OnPushCanceled;
            inputReader.HoldStarted += OnHoldStarted;
            inputReader.HoldCanceled += OnHoldCanceled;
            
        }
    }

    private void OnDisable()
    {
        if (inputReader != null)
        {
            inputReader.PullStarted -= OnPullStarted;
            inputReader.PullCanceled -= OnPullCanceled;
            inputReader.PushStarted -= OnPushStarted;
            inputReader.PushCanceled -= OnPushCanceled;
            inputReader.HoldStarted -= OnHoldStarted;
            inputReader.HoldCanceled -= OnHoldCanceled;
            
        }
    }

    private void OnPullStarted()
    {
        telekinesisManager?.ChangeState(telekinesisManager.GetPullingState());
    }

    private void OnPullCanceled()
    {
    }

    private void OnPushStarted()
    {
        telekinesisManager?.ChangeState(telekinesisManager.GetPushingState());
    }

    private void OnPushCanceled()
    {
    }

    private void OnHoldStarted()
    {
        telekinesisManager?.ChangeState(telekinesisManager.GetHoldingState());
    }

    private void OnHoldCanceled()
    {
    }

  
}

