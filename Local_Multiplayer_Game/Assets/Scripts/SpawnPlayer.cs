using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlayer : MonoBehaviour
{
    public Transform[] spawnPoints; // Spawn points for players
    public Material pinkMaterial; // Pink material for Player 2

    [Header("Audio Sources Feedback")]
    [SerializeField] private AudioSource duckSound;
    [SerializeField] private AudioSource jumpingSound;
    [SerializeField] private AudioSource attackSound;

    private bool[] playerAssigned = new bool[2]; // Tracks if a player is assigned to the joystick
    private PlayerInput[] playerInputs = new PlayerInput[2]; // Store references to the player inputs

    private void OnEnable()
    {
        if (PlayerInputManager.instance != null)
        {
            PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
            Debug.Log("SpawnPlayer subscribed to onPlayerJoined.");
        }
        else
        {
            Debug.LogError("PlayerInputManager instance is missing! Make sure it's added to the scene.");
        }
    }

    private void OnDisable()
    {
        if (PlayerInputManager.instance != null)
        {
            PlayerInputManager.instance.onPlayerJoined -= OnPlayerJoined;
            Debug.Log("SpawnPlayer unsubscribed from onPlayerJoined.");
        }
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        int index = playerInput.playerIndex;

        // Store the reference of the joined player
        playerInputs[index] = playerInput;
        playerInput.gameObject.name = $"Player_{index}";
        Debug.Log($"Player {index} joined as {playerInput.gameObject.name} using {playerInput.devices[0].name}");

        ReadyUpSceneManager.Instance?.PlayerJoined(playerInput.gameObject.name);

        // Assign spawn points and audio setup
        if (index < spawnPoints.Length)
        {
            playerInput.transform.position = spawnPoints[index].position;

            // Handle audio sources setup
            GameObject audioManager = GameObject.FindWithTag("AudioManager");
            if (audioManager != null)
            {
                DontDestroyOnLoad(audioManager);
                SetupAudioSource(playerInput, audioManager);
            }
        }

        // Assign player 1 to the first controller plugged in
        if (index == 0 && playerInputs[0] == null && Gamepad.all.Count > 0)
        {
            playerInputs[0] = playerInput; // Player 1 gets the first plugged-in controller
            Debug.Log("Player 1 assigned to the first controller.");
        }

        // Assign player 2 to the second controller plugged in
        if (index == 1 && playerInputs[1] == null && Gamepad.all.Count > 1)
        {
            playerInputs[1] = playerInput; // Player 2 gets the second plugged-in controller
            Debug.Log("Player 2 assigned to the second controller.");
        }

        // Wait for joystick movement to finalize player assignment
        playerInput.actions["Move"].performed += ctx => OnJoystickMoved(playerInput, ctx);

        DontDestroyOnLoad(playerInput.gameObject);
    }

    private void OnJoystickMoved(PlayerInput playerInput, InputAction.CallbackContext context)
    {
        int playerIndex = playerInput.playerIndex;

        // Ensure joystick movement occurs before assigning player
        if (!playerAssigned[playerIndex])
        {
            Vector2 movement = context.ReadValue<Vector2>();

            // If the right joystick is moved (check magnitude for threshold)
            if (movement.magnitude > 0.1f)
            {
                playerAssigned[playerIndex] = true;
                Debug.Log($"Player {playerIndex + 1} moved the joystick. Assigning and updating LED color.");

                // Assign LED color based on player index (blue for player 1, pink for player 2)
                LEDManager.instance.SetLEDColor(playerIndex);
            }
        }
    }

    private void SetupAudioSource(PlayerInput playerInput, GameObject audioManager)
    {
        // The audio source setup logic remains the same as your original script
        AudioSource duckAudioSource = audioManager.transform.Find("Duck Audio Source")?.GetComponent<AudioSource>();
        if (duckAudioSource != null)
        {
            Ducking duckingScript = playerInput.GetComponent<Ducking>();
            if (duckingScript != null)
            {
                duckingScript.duckingSound = duckAudioSource;
                Debug.Log("Duck Audio Source assigned successfully!");
            }
        }

        // Setup Jump Audio
        AudioSource jumpAudioSource = audioManager.transform.Find("Jump Audio Source")?.GetComponent<AudioSource>();
        if (jumpAudioSource != null)
        {
            Jump jumpingScript = playerInput.GetComponent<Jump>();
            if (jumpingScript != null)
            {
                jumpingScript.jumpingSound = jumpAudioSource;
                Debug.Log("Jump Audio Source assigned successfully!");
            }
        }

        // Setup Attack Audio
        AudioSource attackAudioSource = audioManager.transform.Find("Attack Audio Source")?.GetComponent<AudioSource>();
        if (attackAudioSource != null)
        {
            HorizontalAttack attackingScript = playerInput.GetComponent<HorizontalAttack>();
            if (attackingScript != null)
            {
                attackingScript.attackSound = attackAudioSource;
                Debug.Log("Attack Audio Source assigned successfully!");
            }
        }
    }
}
