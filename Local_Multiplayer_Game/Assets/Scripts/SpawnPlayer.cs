using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlayer : MonoBehaviour
{
    public Transform[] spawnPoints; //Eden: pts where the players will spawn
    public Material pinkMaterial; //Eden: temporary material for Player 2 until we have animations

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

        if (index == 1) //Eden: player 2 index is 1
        {
            Renderer playerRenderer = playerInput.gameObject.GetComponent<Renderer>();
            if (playerRenderer != null && pinkMaterial != null)
            {
                playerRenderer.material = new Material(pinkMaterial); //Eden: if there is a p2 and there is a pink material then set material to pink for p2
                Debug.Log("Applied pink material to Player 2");
            }
            else
            {
                Debug.LogError("Player 2 does not have a Renderer or the pink material is missing.");
            }

            //Eden: Because the player has top part as child I need to change material for the first child
            if (playerInput.transform.childCount > 0)
            {
                Renderer firstChildRenderer = playerInput.transform.GetChild(0).GetComponent<Renderer>();
                if (firstChildRenderer != null && pinkMaterial != null)
                {
                    firstChildRenderer.material = new Material(pinkMaterial);
                    Debug.Log("Applied pink material to Player 2's first child");
                }
                else
                {
                    Debug.LogError("Player 2's first child does not have a Renderer.");
                }
            }

            //Eden: The sword needs to be flipped around therefore I need to move the second child to -1.5 on the x axis
            if (playerInput.transform.childCount > 1)
            {
                Transform secondChild = playerInput.transform.GetChild(1);
                secondChild.localPosition = new Vector3(-1.5f, secondChild.localPosition.y, secondChild.localPosition.z);
            }
        }
    }
}