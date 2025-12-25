using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputReader : MonoBehaviour
{
    private PlayerInputActions inputActions;

    public event Action JumpPerformed;
    public event Action PausePerformed;
    public event Action<Vector2> MovePerformed;

    public event Action<Vector2> AimPerformed;
    public event Action PullStarted;
    public event Action PullCanceled;
    public event Action PushStarted;
    public event Action PushCanceled;
    public event Action HoldStarted;
    public event Action HoldCanceled;
    
    public Vector2 MoveInput { get; private set; }
    public Vector2 AimInput { get; private set; }
    public bool IsPulling { get; private set; }
    public bool IsPushing { get; private set; }
    public bool IsHolding { get; private set; }

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.Jump.performed += OnJump;
        inputActions.Player.Pause.performed += OnPause;

        
        inputActions.Telekinesis.Aim.performed += OnAim;
        inputActions.Telekinesis.Aim.canceled += OnAim;

        inputActions.Telekinesis.Pull.started += OnPullStarted;
        inputActions.Telekinesis.Pull.canceled += OnPullCanceled;

        inputActions.Telekinesis.Push.started += OnPushStarted;
        inputActions.Telekinesis.Push.canceled += OnPushCanceled;

        inputActions.Telekinesis.Hold.started += OnHoldStarted;
        inputActions.Telekinesis.Hold.canceled += OnHoldCanceled;

        
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void OnDestroy()
    {
        
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Jump.performed -= OnJump;
        inputActions.Player.Pause.performed -= OnPause;

        inputActions.Telekinesis.Aim.performed -= OnAim;
        inputActions.Telekinesis.Aim.canceled -= OnAim;
        inputActions.Telekinesis.Pull.started -= OnPullStarted;
        inputActions.Telekinesis.Pull.canceled -= OnPullCanceled;
        inputActions.Telekinesis.Push.started -= OnPushStarted;
        inputActions.Telekinesis.Push.canceled -= OnPushCanceled;
        inputActions.Telekinesis.Hold.started -= OnHoldStarted;
        inputActions.Telekinesis.Hold.canceled -= OnHoldCanceled;
        
    }

    
    private void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
        MovePerformed?.Invoke(MoveInput);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        JumpPerformed?.Invoke();
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        PausePerformed?.Invoke();
    }

    
    private void OnAim(InputAction.CallbackContext context)
    {
        AimInput = context.ReadValue<Vector2>();
        AimPerformed?.Invoke(AimInput);
    }

    private void OnPullStarted(InputAction.CallbackContext context)
    {
        IsPulling = true;
        PullStarted?.Invoke();
    }

    private void OnPullCanceled(InputAction.CallbackContext context)
    {
        IsPulling = false;
        PullCanceled?.Invoke();
    }

    private void OnPushStarted(InputAction.CallbackContext context)
    {
        IsPushing = true;
        PushStarted?.Invoke();
    }

    private void OnPushCanceled(InputAction.CallbackContext context)
    {
        IsPushing = false;
        PushCanceled?.Invoke();
    }

    private void OnHoldStarted(InputAction.CallbackContext context)
    {
        IsHolding = true;
        HoldStarted?.Invoke();
    }

    private void OnHoldCanceled(InputAction.CallbackContext context)
    {
        IsHolding = false;
        HoldCanceled?.Invoke();
    }

    

    
    public void EnablePlayerInput()
    {
        inputActions.Player.Enable();
    }

    public void DisablePlayerInput()
    {
        inputActions.Player.Disable();
    }

    public void EnableTelekinesisInput()
    {
        inputActions.Telekinesis.Enable();
    }

    public void DisableTelekinesisInput()
    {
        inputActions.Telekinesis.Disable();
    }

    public void EnableUIInput()
    {
        inputActions.UI.Enable();
    }

    public void DisableUIInput()
    {
        inputActions.UI.Disable();
    }
}
