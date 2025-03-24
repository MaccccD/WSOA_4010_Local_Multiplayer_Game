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
        {
            playerInput.transform.position = spawnPoints[index].position;

            // Retrieve existing AudioSources from the Player prefab as opposed to adding new ones 
            AudioSource[] audioSources = playerInput.gameObject.GetComponents<AudioSource>();

            if (audioSources.Length >= 3)
            {
                duckSound = audioSources[0];
                jumpingSound = audioSources[1];
                attackSound = audioSources[2];
            }
            else
            {
                Debug.LogError("Not enough AudioSources found on the Player prefab.");
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
