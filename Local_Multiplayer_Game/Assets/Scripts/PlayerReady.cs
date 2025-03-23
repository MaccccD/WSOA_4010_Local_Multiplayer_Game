using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerReady : MonoBehaviour
{
    // A flag to ensure we only mark this player as ready once.
    private bool isReady = false;

    /// <summary>
    /// This callback will be called when the "Duck" (or Ready) action is performed.
    /// In your Input Action Asset, bind the square (or your designated button) to this action.
    /// </summary>
    /// <param name="context">The callback context from the Input System.</param>
    public void OnReady(InputAction.CallbackContext context)
    {
        // Check if the action was performed (i.e. the button was pressed).
        if (context.performed && !isReady)
        {
            isReady = true;
            Debug.Log($"{gameObject.name} pressed Ready and is now ready!");

            // Report to the central ready-up manager.
            // Here we assume the central manager (e.g., ReadyUpSceneManager) has a method to mark players as ready.
            ReadyUpSceneManager.Instance?.PlayerIsReady(gameObject.name);
        }
    }
}
