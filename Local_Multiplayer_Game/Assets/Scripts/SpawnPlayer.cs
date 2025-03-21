using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlayer : MonoBehaviour
{
    public Transform[] spawnPoints; //Eden: pts where the players will spawn

    private IEnumerator WaitForPlayerInputManager()
    {
        while (PlayerInputManager.instance == null)
        {
            yield return null;
        }

        PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
    }

    private void OnEnable()
    {
        if (PlayerInputManager.instance != null)
        {
            PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined; //Eden: listen for new joined players
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
        }
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        int index = playerInput.playerIndex;

        //Eden: this assign a unique name to each player to ensure they are seperately named (idk if this also fixed the controller problem) 
        playerInput.gameObject.name = $"Player_{index}";

        Debug.Log($"Player {index} joined as {playerInput.gameObject.name} using {playerInput.devices[0].name}");

        if (index < spawnPoints.Length)
        {
            playerInput.transform.position = spawnPoints[index].position;
        }
    }

}