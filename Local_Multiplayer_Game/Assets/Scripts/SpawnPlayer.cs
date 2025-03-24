using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlayer : MonoBehaviour
{

    // line number 1 from Chatgpt (Accessed: 24 March 2024) like for exmaple 
    public Transform[] spawnPoints;
    public Material pinkMaterial;

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
        int index = playerInput.playerIndex;

        playerInput.gameObject.name = $"Player_{index}";
        Debug.Log($"Player {index} joined as {playerInput.gameObject.name} using {playerInput.devices[0].name}");

        ReadyUpSceneManager.Instance?.PlayerJoined(playerInput.gameObject.name);

        //Dumi: The audio sources were getting spawned but they were missing the actual component so i added the compnent of the audio manually ,
        //that way the audio sources are always assigned.
        if (index < spawnPoints.Length && Input.GetKeyDown(KeyCode.Space))
        {
            playerInput.transform.position = spawnPoints[index].position;

            // Dumi: Handle audio sources setup. I separated the audio sources from being children of the player to be seperate outside of the player.
            GameObject audioManager = GameObject.FindWithTag("AudioManager");
            Debug.Log(audioManager + " has been found!!");

            if (audioManager != null)
            {
                DontDestroyOnLoad(audioManager);
                // Find and assign the Duck Audio Source
                AudioSource duckAudioSource = audioManager.transform.Find("Duck Audio Source")?.GetComponent<AudioSource>();
                if (duckAudioSource != null)
                {
                    Ducking duckingScript = playerInput.GetComponent<Ducking>();
                    if (duckingScript != null)
                    {
                        duckingScript.duckingSound = duckAudioSource;
                        Debug.Log("Duck Audio Source assigned successfully! (1)");
                    }
                    else
                    {
                        Debug.LogError("Ducking script not found on the player object.");
                    }
                }
                else
                {
                    Debug.LogError("Duck Audio Source not found in AudioManager!");
                }

                // Dumi: Jump Audio setup
                AudioSource jumpAudioSource = audioManager.transform.Find("Jump Audio Source")?.GetComponent<AudioSource>();
                if (jumpAudioSource != null)
                {
                    Jump jumpingScript = playerInput.GetComponent<Jump>();
                    if (jumpingScript != null)
                    {
                        jumpingScript.jumpingSound = jumpAudioSource;
                        Debug.Log("Jump Audio Source assigned successfully! (2)");
                    }
                    else
                    {
                        Debug.LogError("Jumping script not found on the player object.");
                    }
                }
                else
                {
                    Debug.LogError("Jump Audio Source not found in AudioManager!");
                }

                // Dumi: Attack Audio setup
                AudioSource attackAudioSource = audioManager.transform.Find("Attack Audio Source")?.GetComponent<AudioSource>();
                if (attackAudioSource != null)
                {
                    HorizontalAttack attackingScript = playerInput.GetComponent<HorizontalAttack>();
                    if (attackingScript != null)
                    {
                        attackingScript.attackSound = attackAudioSource;
                        Debug.Log("Attack Audio Source assigned successfully! (3)");
                    }
                    else
                    {
                        Debug.LogError("Attacking script not found on the player object.");
                    }
                }
                else
                {
                    Debug.LogError("Attack Audio Source not found in AudioManager!");
                }

            }
            else
            {
                Debug.LogWarning($"No spawn point defined for player {index}. and no audio srouces have been assigned");
            }

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

                if (playerInput.transform.childCount > 1)
                {
                    Transform secondChild = playerInput.transform.GetChild(1);
                    secondChild.localPosition = new Vector3(-1.5f, secondChild.localPosition.y, secondChild.localPosition.z);
                }
            }
            DontDestroyOnLoad(playerInput.gameObject);

        }
    }
}
