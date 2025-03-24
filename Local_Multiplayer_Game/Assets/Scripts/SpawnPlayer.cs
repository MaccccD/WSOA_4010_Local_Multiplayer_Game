using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlayer : MonoBehaviour
{
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
        if (index < spawnPoints.Length)
        // Eden: Spawn the player visually by positioning at the corresponding spawn point.
   
        if (index < spawnPoints.Length)
        {
            playerInput.transform.position = spawnPoints[index].position;

            //Dumi: Since the audio sources were getting spwaned but the sound wasnt playing i took out the audio sources as child of the player and set them up separetely.
            GameObject audioManager = GameObject.FindWithTag("AudioManager");
            Debug.Log(audioManager + "hass been found!!");

            if (audioManager != null)
            {
                //D: Logging in  and searching  for all audio sources at once so its  repetetive
                Debug.Log(audioManager + " has been found!!");
                //D:  Find the Duck Audio Source comp 1stly
                AudioSource duckAudioSource = audioManager.transform.Find("Duck Audio Source")?.GetComponent<AudioSource>();

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
                if (duckAudioSource != null)
                {
                    //D:  Get the Ducking script from the spawned player object since it is an instance of the ori player, it should spawn with the script already.
                    Ducking duckingScript = playerInput.GetComponent<Ducking>();

                    if (duckingScript != null)
                    {
                        //D:  Assigning the Duck Audio Source to the Ducking script's duckingSound variable
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
                    Debug.LogError("AudioManager GameObject not found in the scene!");
                }

            if (playerInput.transform.childCount > 1)
            {
                Transform secondChild = playerInput.transform.GetChild(1);
                secondChild.localPosition = new Vector3(-1.5f, secondChild.localPosition.y, secondChild.localPosition.z);
                // Dumi : this is the jump audio stuff:
                if (audioManager != null)
                {

                    AudioSource jumpAudioSource = audioManager.transform.Find("Jump Audio Source ")?.GetComponent<AudioSource>();

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
                        Debug.LogError("AudioManager GameObject not found in the scene!");
                    }
                    //D: Attack Audio stuff here :
                    if (audioManager != null)
                    {

                        AudioSource attackAudioSource = audioManager.transform.Find("Attack Audio Source")?.GetComponent<AudioSource>();

                        if (attackAudioSource != null)
                        {

                            HorizontalAttack attackingScript = playerInput.GetComponent<HorizontalAttack>();

                            if (attackingScript != null)
                            {

                                attackingScript.attackSound = attackAudioSource;
                                Debug.Log("Attack  Audio Source assigned successfully! (3)");
                            }
                            else
                            {
                                Debug.LogError("Attacking script not found on the player object.");
                            }
                        }
                        else
                        {
                            Debug.LogWarning($"No spawn point defined for player {index}");
                        }

                        // E: For Player 2 (index == 1), apply the pink material and adjust children.
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

                            //E: Change the material for the first child if available.
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

                            // E: Adjust the second child's local position (for example, to flip a sword).
                            if (playerInput.transform.childCount > 1)
                            {
                                Transform secondChild = playerInput.transform.GetChild(1);
                                secondChild.localPosition = new Vector3(-1.5f, secondChild.localPosition.y, secondChild.localPosition.z);
                            }
                        }
                    }
                }
            }
        }
        DontDestroyOnLoad(playerInput.gameObject);

    }
}

    }
}