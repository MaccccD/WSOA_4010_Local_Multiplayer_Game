using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlayer : MonoBehaviour
{
    public Transform[] spawnPoints; // Points where the players will be spawned.
    public Material pinkMaterial;   // Temporary material for Player 2.

    // No need for an "isJoinScene" flag if we want to both spawn and update UI.
    // (Remove any previous isJoinScene logic.)

    [Header("Audio  Sources Feedback")]
    [SerializeField] private AudioSource duckSound;
    [SerializeField] private AudioSource jumpingSound;
    [SerializeField] private AudioSource attackSound;




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
        // Get the built-in playerIndex.
        int index = playerInput.playerIndex;

        // Rename the player GameObject (e.g., "Player_0", "Player_1", etc.).
        playerInput.gameObject.name = $"Player_{index}";
        Debug.Log($"Player {index} joined as {playerInput.gameObject.name} using {playerInput.devices[0].name}");

        // Report to the central ReadyUpSceneManager so that the proper UI is activated.
        // This will toggle the default UI image for that player (e.g., eyes.png).
        ReadyUpSceneManager.Instance?.PlayerJoined(playerInput.gameObject.name);

        // Spawn the player visually by positioning at the corresponding spawn point.
        //Dumi: The audio sources were getting spawned but they were missing the actual component so i added the compnent of the audio manually ,
        //that way the audio sources are always assigned.
        if (index < spawnPoints.Length)
        {
            playerInput.transform.position = spawnPoints[index].position;

            duckSound = playerInput.gameObject.GetComponent<AudioSource>();
            attackSound = playerInput.gameObject.GetComponent<AudioSource>();
            jumpingSound = playerInput.gameObject.GetComponent<AudioSource>();

            Debug.Log($"DuckSound: {duckSound}, AttackSound: {attackSound}, JumpingSound: {jumpingSound}");

            if (duckSound == null)
            {
                duckSound = playerInput.gameObject.AddComponent<AudioSource>();
                Debug.Log(duckSound + "has been added sucessfully (1)");
              
            }
            if(attackSound == null)
            {
                attackSound = playerInput.gameObject.AddComponent<AudioSource>();
                Debug.Log(attackSound + "has been added successfully(2)");
            }
            if(jumpingSound == null)
            {
                jumpingSound = playerInput.gameObject.AddComponent<AudioSource>();
                Debug.Log(jumpingSound + "has been added successfully(3)");
            }
        }
        else
        {
            Debug.LogWarning($"No spawn point defined for player {index}. and no audio srouces have been assigned");
        }

        // For Player 2 (index == 1), apply the pink material and adjust children.
        if (index == 1)
        {
            Renderer playerRenderer = playerInput.gameObject.GetComponent<Renderer>();
            if (playerRenderer != null && pinkMaterial != null)
            {
                playerRenderer.material = new Material(pinkMaterial);
                Debug.Log("Applied pink material to Player 2.");
            }
            else
            {
                Debug.LogError("Player 2 does not have a Renderer or the pink material is missing.");
            }

            // Change the material for the first child if available.
            if (playerInput.transform.childCount > 0)
            {
                Renderer firstChildRenderer = playerInput.transform.GetChild(0).GetComponent<Renderer>();
                if (firstChildRenderer != null && pinkMaterial != null)
                {
                    firstChildRenderer.material = new Material(pinkMaterial);
                    Debug.Log("Applied pink material to Player 2's first child.");
                }
                else
                {
                    Debug.LogError("Player 2's first child does not have a Renderer.");
                }
            }

            // Adjust the second child's local position (for example, to flip a sword).
            if (playerInput.transform.childCount > 1)
            {
                Transform secondChild = playerInput.transform.GetChild(1);
                secondChild.localPosition = new Vector3(-1.5f, secondChild.localPosition.y, secondChild.localPosition.z);
            }
        }
    }
}
