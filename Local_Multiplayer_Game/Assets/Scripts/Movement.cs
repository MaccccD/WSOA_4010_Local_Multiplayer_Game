using UnityEngine;
using UnityEngine.InputSystem;
public class Movement : MonoBehaviour
{

    public InputActionAsset inputActions;
    public float moveSpeed = 5f;

    private InputAction moveAction;
    private Vector2 moveInput;
    public Ducking duckingMechanic;
    public Jump jumpingMechanic;


    private void Start()
    {
        duckingMechanic = GetComponent<Ducking>();
        jumpingMechanic = GetComponent<Jump>();
       Debug.Log(jumpingMechanic + "script has been foundddd");
    }
    private void OnEnable()
    {
        // Find the action map and move action
        var actionMap = inputActions.FindActionMap("Player_1");
        moveAction = actionMap.FindAction("Movement");

        // Enable input actions
        actionMap.Enable();
        moveAction.Enable();

        // Subscribe to input performed/canceled events
        moveAction.performed += OnMove;
        moveAction.canceled += OnMoveCancelled;
    }

    private void OnDisable()
    {
        // Disable input actions and unsubscribe events
        moveAction.performed -= OnMove; // This listens for when the player presses a movement key like a or d
        moveAction.canceled -= OnMoveCancelled; // This listens for when the player releases the key
        moveAction.Disable(); // It turns off the input action -  Unity will stop listening for that specific input. Improves performance by stopping unnecessary input checks.
    }

    private void OnMove(InputAction.CallbackContext context) // difference between the context of the controller or the keyboard
    {
        // Read movement input from the player when performed ^
        moveInput = context.ReadValue<Vector2>(); // as a vector2 . Context ==> WASD or the gamepad controller
    }

    private void OnMoveCancelled(InputAction.CallbackContext context) // the context ad an argument passed in here
    {
        // Reset movement when input is cancelled ^
        moveInput = Vector2.zero; // 
    }

    private void Update()
    {
        // Apply movement based on input
        Vector2 movement = new Vector2(moveInput.x, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
        // this is where im referencing the ducking mechanic as well as the jumping mechanic :
        duckingMechanic.Duck();
        jumpingMechanic.CheckGroundStatus();
        jumpingMechanic.JumpMech();
        
    }

}

