using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlayer : MonoBehaviour
{
    public Transform[] spawnPoints; //Eden: pts where the players will spawn
    private int playerCount = 0; //Eden: count how many players join

    private IEnumerator WaitForPlayerInputManager()
    {
        while (PlayerInputManager.instance == null)
        {
            yield return null; // Wait for the next frame
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

    private void OnPlayerJoined(PlayerInput playerInput) //Eden: triggered when a player joins
    {
        if (playerCount < spawnPoints.Length)
        {
            playerInput.transform.position = spawnPoints[playerCount].position; //Eden: if enough spawn pts player pos set to spawn point pos
        }
        else
        {
            Debug.LogWarning("Not enough spawn pts"); //Eden: if not enough spawn pts log this message
        }

        playerCount++; //Eden: player count variable increases (ensures next player spawns at next position)
    }
}