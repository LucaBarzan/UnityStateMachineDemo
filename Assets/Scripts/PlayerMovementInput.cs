using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementInput : MonoBehaviour
{
    public Vector2 InputDirection { get; private set; }

    [SerializeField] InputActionReference input_Movement;

    void Update()
    {
        if (input_Movement.action.enabled)
            InputDirection = input_Movement.action.ReadValue<Vector2>();
    }
}