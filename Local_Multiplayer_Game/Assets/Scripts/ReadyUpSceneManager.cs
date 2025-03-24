using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

//Eden: this script basically keeps track of the players in scene and if theyre readied up then change between scenes
public class ReadyUpSceneManager : MonoBehaviour
{
    //eden: instance to use in other scripts
    public static ReadyUpSceneManager Instance;

    [Header("Settings")]
    public int requiredPlayers = 2; //Eden: both (2) players are required to be ready before changing scenes
    public float sceneSwitchDelay = 3f; //Eden: 3 second delay to allow the players to see the feedback UI

    [Header("Player 1 UI")]
    public GameObject p1DefaultImage; //Eden: this is the png eyes looking forward for p1
    public GameObject p1ReadyImage; //Eden: this is the png blue eyes looking right to show UI READY ststus  p1

    [Header("Player 2 UI")]
    public GameObject p2DefaultImage; //Eden: this is the png eyes looking forward for p2
    public GameObject p2ReadyImage; //Eden: this is the png pink eyes looking lefr to show UI READY ststus p2

    //Eden: in the first scene mainly (bcs player prefabs are dontdestroyonload) and other scenes we need to keep track of the players joined names
    private HashSet<string> joinedPlayers = new HashSet<string>();
    //Eden: and keep track of which players pressed ready
    private HashSet<string> readyPlayers = new HashSet<string>();

    //Eden: this bool tracks if transition happened so we only trigger the transition once
    private bool sceneTransitionTriggered = false;

    private void Awake()
    {

        //Eden this just makes sure there is only one of these scripts in the scene at a time
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //Eden: find all objects that have PlayerInput component (we do this because we only "join" on first scene and all other scenes the players are already there so we find them)
        PlayerInput[] existingPlayers = FindObjectsByType<PlayerInput>(FindObjectsSortMode.None);
        
        //Eden: we can now treat the players as "joined" players 
        foreach (PlayerInput playerInput in existingPlayers)
        {
            string playerName = playerInput.gameObject.name;
            Debug.Log("[ReadyUpSceneManager] Found persistent player: " + playerName);
            PlayerJoined(playerName);
        }
    }

    //Eden: this is the funct that keeps track of players who have joined 
    public void PlayerJoined(string playerName)
    {
        Debug.Log($"PlayerJoined called with playerName: {playerName}");
        if (!joinedPlayers.Contains(playerName)) //Eden: only carry on if the playername is not one we already have as joined
        {
            joinedPlayers.Add(playerName);
            Debug.Log($"Player {playerName} joined. Total joined: {joinedPlayers.Count}/{requiredPlayers}");

            //Eden: if a player has joined ie active in scene activate the forward looking eyes png
            if (playerName.Equals("Player_0"))
            {
                if (p1DefaultImage != null) p1DefaultImage.SetActive(true);
                else Debug.LogError("p1DefaultImage is not assigned!");
            }
            else if (playerName.Equals("Player_1"))
            {
                if (p2DefaultImage != null) p2DefaultImage.SetActive(true);
                else Debug.LogError("p2DefaultImage is not assigned!");
            }
            else
            {
                Debug.LogWarning($"No UI set up for {playerName}");
            }
        }
    }

    //Eden this funct checks if players are ready and can make scene changes
    public void PlayerIsReady(string playerName)
    {
        if (!readyPlayers.Contains(playerName)) //Eden: if player hasnt readied up already then
        {
            readyPlayers.Add(playerName); //Eden: set player to ready
            Debug.Log($"Player {playerName} is ready. ({readyPlayers.Count}/{requiredPlayers})");

            //Eden: if a player is ready change the eyes png looking forward to the yes looking at opponent
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

        //Eden: check if both players are ready to move to next scene
        CheckAllPlayersReady();
    }

    //Eden: this funct checks if both players are ready and if so depending on currentscene switch to specific scenes
    private void CheckAllPlayersReady()
    {
        //Eden: when the above is true change to scene and set below bool true to only trigger transition once
        if (readyPlayers.Count >= requiredPlayers && !sceneTransitionTriggered) 
        {
            sceneTransitionTriggered = true;
            Debug.Log("All players are ready. Transitioning in " + sceneSwitchDelay + " seconds.");
            StartCoroutine(LoadNextSceneAfterDelay(sceneSwitchDelay));
        }
    }

    //Eden: this loads the next scene depending on current scene (after delay time)
    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        string currentScene = SceneManager.GetActiveScene().name;
        string nextScene = "";
        if (currentScene == "MainMenu") nextScene = "StartScreen";
        else if (currentScene == "StartScreen") nextScene = "FightScreen";
        else nextScene = "FightScreen";
        Debug.Log("Loading scene: " + nextScene);
        SceneManager.LoadScene(nextScene);
    }
}
