using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : CharacterMovement
{
    [SerializeField] private JumpState jumpState;
    [SerializeField] private InputActionReference input_Jump;
    [SerializeField] private InputActionReference input_Slide;

    protected override void Awake()
    {
        base.Awake();
        AddState(jumpState);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        jumpState.OnCompleted += JumpState_OnCompleted;
        SubscribeToInputEvents();
    }

    protected override void Update()
    {
        base.Update();

        if (jumpState.CanBufferJump())
            SetCurrentState(jumpState);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        UnsubscribeFromInputEvents();
    }

    private void SubscribeToInputEvents()
    {
        input_Jump.action.started += OnJumpInput_started;
        input_Jump.action.canceled += OnJumpInput_canceled;
        input_Slide.action.performed += OnSlideInput_performed;
    }
    private void UnsubscribeFromInputEvents()
    {
        input_Jump.action.performed -= OnJumpInput_started;
        input_Jump.action.canceled -= OnJumpInput_canceled;
        input_Slide.action.performed -= OnSlideInput_performed;
    }

    private void OnSlideInput_performed(InputAction.CallbackContext obj)
    {

    }


    private void OnJumpInput_started(InputAction.CallbackContext obj)
    {
        jumpState.OnJumpInputPressed();

        if (jumpState.CanJump() || jumpState.CanCoyoteJump())
            SetCurrentState(jumpState);
    }

    private void OnJumpInput_canceled(InputAction.CallbackContext obj)
    {
        // Interrupt jump state if we are currently in the jump state
        // Temporarely modify gravity to allows for variable jump height based on how long the player holds the jump input
        if (jumpState.enabled)
        {
            airborneState.SetGravity(jumpState.EarlyStopGravity);
            jumpState.OnJumpInputReleased();
        }
    }
    private void JumpState_OnCompleted() => SelectState();

    protected override void SelectState()
    {
        if (!jumpState.enabled)
            base.SelectState();
    }
}
