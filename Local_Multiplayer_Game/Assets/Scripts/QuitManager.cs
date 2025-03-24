using UnityEngine;
using UnityEngine.InputSystem;

public class QuitManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction quitAction;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        playerInput = GetComponent<PlayerInput>();

        quitAction = playerInput.actions["Quit"];
    }

    private void OnEnable()
    {
        quitAction.started += OnQuitPressed;
    }

    private void OnDisable()
    {
        quitAction.started -= OnQuitPressed;
    }

    private void OnQuitPressed(InputAction.CallbackContext context)
    {
        QuitGame();
    }

    private void QuitGame()
    {
        Debug.Log("Quit button pressed. Exiting the game");
        Application.Quit(); 

    }
}
