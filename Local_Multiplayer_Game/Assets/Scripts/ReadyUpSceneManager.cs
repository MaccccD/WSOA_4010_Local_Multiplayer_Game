using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadyUpSceneManager : MonoBehaviour
{
    public static ReadyUpSceneManager Instance;

    [Header("Settings")]
    public int requiredPlayers = 2;
    public float sceneSwitchDelay = 3f;

    [Header("Player 1 UI")]
    public GameObject p1DefaultImage;
    public GameObject p1ReadyImage;

    [Header("Player 2 UI")]
    public GameObject p2DefaultImage;
    public GameObject p2ReadyImage;

    private HashSet<string> joinedPlayers = new HashSet<string>();
    private HashSet<string> readyPlayers = new HashSet<string>();
    private bool sceneTransitionTriggered = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayerJoined(string playerName)
    {
        Debug.Log($"PlayerJoined called with playerName: {playerName}");
        if (!joinedPlayers.Contains(playerName))
        {
            joinedPlayers.Add(playerName);
            Debug.Log($"Player {playerName} has joined. Total joined: {joinedPlayers.Count}/{requiredPlayers}");

            if (playerName.Equals("Player_0"))
            {
                if (p1DefaultImage != null)
                {
                    p1DefaultImage.SetActive(true);
                    Debug.Log("Activated Player 1 Default UI.");
                }
                else
                {
                    Debug.LogError("p1DefaultImage is not assigned in the Inspector!");
                }
            }
            else if (playerName.Equals("Player_1"))
            {
                if (p2DefaultImage != null)
                {
                    p2DefaultImage.SetActive(true);
                    Debug.Log("Activated Player 2 Default UI.");
                }
                else
                {
                    Debug.LogError("p2DefaultImage is not assigned in the Inspector!");
                }
            }
            else
            {
                Debug.LogWarning($"No UI set up for {playerName}");
            }
        }
    }

    public void PlayerIsReady(string playerName)
    {
        if (!readyPlayers.Contains(playerName))
        {
            readyPlayers.Add(playerName);
            Debug.Log($"Player {playerName} is ready. ({readyPlayers.Count}/{requiredPlayers})");

            if (playerName.Equals("Player_0"))
            {
                if (p1DefaultImage != null) p1DefaultImage.SetActive(false);
                if (p1ReadyImage != null) p1ReadyImage.SetActive(true);
            }
            else if (playerName.Equals("Player_1"))
            {
                if (p2DefaultImage != null) p2DefaultImage.SetActive(false);
                if (p2ReadyImage != null) p2ReadyImage.SetActive(true);
            }
            else
            {
                Debug.LogWarning($"No UI set up for {playerName}");
            }
        }

        CheckAllPlayersReady();
    }

    private void CheckAllPlayersReady()
    {
        if (readyPlayers.Count >= requiredPlayers && !sceneTransitionTriggered)
        {
            sceneTransitionTriggered = true;
            Debug.Log("All players are ready. Transitioning in " + sceneSwitchDelay + " seconds.");
            StartCoroutine(LoadNextSceneAfterDelay(sceneSwitchDelay));
        }
    }

    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("FightScreen"); 
    }
}
