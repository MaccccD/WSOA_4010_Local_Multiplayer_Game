using UnityEngine;
using UnityEngine.UI; 

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
    private int player1Health = 100;
    private int player2Health = 100;

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
    private void HandlePlayerHit(int hitPlayerIndex)
    {
        if (hitPlayerIndex == 0) //Eden: if p1 was hit
        {
            player1Health -= 10; //Eden: take 10 off p1 health int
            player1Health = Mathf.Max(player1Health, 0); //Eden: this prevent negative health
        }
        else if (hitPlayerIndex == 1) //Eden: or if p2 was hit
        {
            player2Health -= 10; //Eden: take 10 off p2 health int
            player2Health = Mathf.Max(player2Health, 0); //Eden: this prevent negative health
        }

        //Eden: update the UI to reflect current health values
        UpdateUI();
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
}
