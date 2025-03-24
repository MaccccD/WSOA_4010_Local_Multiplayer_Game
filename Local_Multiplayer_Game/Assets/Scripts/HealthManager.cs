using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    //Eden: These texts will change with the player health ints
    public Text player1HealthText;
    public Text player1ChildText;
    public Slider player1HealthSlider;

    public Text player2HealthText;
    public Text player2ChildText;
    public Slider player2HealthSlider;


    //Eden: integers to keep track of health values
    public int player1Health = 100;
    public int player2Health = 100;

    //Eden: the following allows me to access these values in other scripts
    public int Player1Health { get { return player1Health; } }
    public int Player2Health { get { return player2Health; } }

    //Dumi: Adding the particle effect here bc it needs to play only after a play has been hit and this is where the hit logic is handled:
    [Header("Particle Effects")]
    public GameObject Blood;

    //Eden: When Sword is enabled (bcs this script is attached to sword) 
    private void OnEnable()
    {
        SwordCollision.OnPlayerHitEvent += HandlePlayerHit; //Eden: When sword collision event happens call HandlePlayerHit function
        UpdateUI(); //Eden: and update function (for initial values)
    }

    //Eden: When Sword is disabled (bcs this script is attached to sword) 
    private void OnDisable()
    {
        SwordCollision.OnPlayerHitEvent -= HandlePlayerHit; //Eden: stop event (no mem leaks)
    }

    //Eden: This funct is called when hit event occurs. hitPlayerIndex is for which player was hti
    private void HandlePlayerHit(int hitPlayerIndex, Vector3 hitPosition)
    {
        if (hitPlayerIndex == 0) //Eden: if p1 was hit
        {
            player1Health -= 5; //Eden: take 10 off p1 health int
            //Instantiate(Blood, transform.position, Quaternion.identity);  // Dumi: so each time a player has been hit, the player takes damage and the we visually communicate that by playing the blood splash effect. I'm instantiating bc the hits will happen multiple times and its easuer t habdle the re-spawn logic using Instantiate.
            player1Health = Mathf.Max(player1Health, 0); //Eden: this prevent negative health
        }
        else if (hitPlayerIndex == 1) //Eden: or if p2 was hit
        {
            player2Health -= 5; //Eden: take 10 off p2 health int
            //Instantiate(Blood, transform.position, Quaternion.identity); // Dumi : same as above 
            player2Health = Mathf.Max(player2Health, 0); //Eden: this prevent negative health
        }

        Vector3 spawnPosition = hitPosition + new Vector3(0, 1, 0); //Eden: add one unit to the y axis to place blood where sword hits
        GameObject bloodInstance = Instantiate(Blood, spawnPosition, Quaternion.identity);
        Debug.Log("Blood spawned at: " + bloodInstance.transform.position);


        //Eden: update the UI to reflect current health values
        UpdateUI();

        if (player1Health == 0)
        {
            PlayerPrefs.SetInt("Loser", 0); 
            ActivateWinLoseScreen();
        }
        else if (player2Health == 0)
        {
            PlayerPrefs.SetInt("Loser", 1); 
            ActivateWinLoseScreen();
        }
    }

    //Eden: updates Ui based on int values
    private void UpdateUI()
    {
        if (player1HealthText != null)
        {
            player1HealthText.text = "Player 1: " + player1Health.ToString();
            player1ChildText.text = "Player 1: " + player1Health.ToString();
        }
        if (player1HealthSlider != null)
        {
            player1HealthSlider.value = player1Health;

        }

        if (player2HealthText != null)
        {
            player2HealthText.text = "Player 2: " + player2Health.ToString();
            player2ChildText.text = "Player 2: " + player2Health.ToString();
        }
        if (player2HealthSlider != null)
        {
            player2HealthSlider.value = player2Health;

        }
    }

    private void ActivateWinLoseScreen()
    {
        SceneManager.LoadScene("WinLoseScreen");
    }
}
