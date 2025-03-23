using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoinManager : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(WaitForPlayerInputManager());
    }

    private IEnumerator WaitForPlayerInputManager()
    {
        // Wait until the PlayerInputManager instance is available.
        while (PlayerInputManager.instance == null)
        {
            yield return null;
        }
        PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
        Debug.Log("JoinManager subscribed to onPlayerJoined.");
    }

    private void OnDisable()
    {
        if (PlayerInputManager.instance != null)
        {
            PlayerInputManager.instance.onPlayerJoined -= OnPlayerJoined;
            Debug.Log("JoinManager unsubscribed from onPlayerJoined.");
        }
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        // Assign a unique index using the persistent manager.
        int assignedIndex = PlayerIndexManager.instance.AssignPlayerIndex(playerInput);
        Debug.Log($"[JoinManager] Player {assignedIndex} joined using device: {playerInput.devices[0].name}");
    }
}
