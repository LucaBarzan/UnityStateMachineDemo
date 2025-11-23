using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [SerializeField] InputActionReference input_Jump;
    [SerializeField] InputActionReference input_Slide;
    [SerializeField] SurfaceContactSensor contactSensor;
    [SerializeField] GroundedState groundedState;
    [SerializeField] AirborneState airborneState;
    [SerializeField] JumpState jumpState;

    //[SerializeField] ;

    private readonly StateMachine stateMachine = new StateMachine();

    private void Awake()
    {
        stateMachine.Set(airborneState);
    }

    private void OnEnable()
    {
        SubscribeToInputEvents();
    }

    private void Start()
    {

    }

    private void Update()
    {
        SelectState();
    }

    private void FixedUpdate()
    {

    }

    private void OnDisable()
    {
        UnsubscribeFromInputEvents();
    }

    private void SubscribeToInputEvents()
    {
        input_Jump.action.performed += OnJumpInput_performed;
        input_Slide.action.performed += OnSlideInput_performed;
    }
    private void UnsubscribeFromInputEvents()
    {
        input_Jump.action.performed -= OnJumpInput_performed;
        input_Slide.action.performed -= OnSlideInput_performed;
    }

    private void OnSlideInput_performed(InputAction.CallbackContext obj)
    {

    }

    private void OnJumpInput_performed(InputAction.CallbackContext obj)
    {
        stateMachine.Set(jumpState);
    }

    private void SelectState()
    {
        if (!stateMachine.State.IsComplete)
            return;

        if (contactSensor.GroundHit)
        {
            stateMachine.Set(groundedState);
        }
        else
        {
            stateMachine.Set(airborneState);
        }
    }

    private void SetControllable(bool controllable)
    {
        var inputManager = InputManager.Instance;

        if (inputManager == null)
            return;

        if (controllable && !inputManager.PlayerMap.enabled)
        {
            inputManager.PlayerMap.Enable();
        }
        else if (!controllable && inputManager.PlayerMap.enabled)
        {
            inputManager.PlayerMap.Disable();
        }
    }
}