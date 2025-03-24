using UnityEngine;

public class VidPlayer : MonoBehaviour
{
    [Header("Game Objects for Video Activation")]
    public GameObject pinkVid; //Eden: Player 2's win video
    public GameObject blueVid; //Eden: Player 1's win video

    private void Start()
    {
        //Eden: get the loser player from PlayerPrefs
        int loserPlayerIndex = PlayerPrefs.GetInt("Loser", -1);

        if (loserPlayerIndex == 0) //Eden: player 1 lost
        {
            if (pinkVid != null)
                pinkVid.SetActive(true); //Eden Ativate Player 2's win video
        }
        else if (loserPlayerIndex == 1) //Eden: player 2 lost
        {
            if (blueVid != null)
                blueVid.SetActive(true); //Eden: Activate Player 1's win video
        }
    }
}
