using UnityEngine;

public class VidPlayer : MonoBehaviour
{
    [Header("Game Objects for Video Activation")]
    public GameObject pinkVid; // Player 1's lose video
    public GameObject blueVid; // Player 2's lose video

    private void Start()
    {
        // Get the loser player from PlayerPrefs
        int loserPlayerIndex = PlayerPrefs.GetInt("Loser", -1);

        if (loserPlayerIndex == 0) // Player 1 lost
        {
            if (pinkVid != null)
                pinkVid.SetActive(true); // Activate Player 1's lose video
        }
        else if (loserPlayerIndex == 1) // Player 2 lost
        {
            if (blueVid != null)
                blueVid.SetActive(true); // Activate Player 2's lose video
        }
    }
}
