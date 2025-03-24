using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class LEDManager : MonoBehaviour
{
    public static LEDManager instance; // To ensure only one instance exists

    private void Awake()
    {
        // Ensure only one instance of this object exists
        if (instance != null)
        {
            Destroy(gameObject); // Destroy this instance if one already exists
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
    }

    // Method to change LED colors based on the player's index
    public void SetLEDColor(int playerIndex)
    {
        if (Gamepad.all.Count > playerIndex)
        {
            var gamepad = (DualSenseGamepadHID)Gamepad.all[playerIndex];

            if (gamepad != null)
            {
                Color ledColor;

                // Use hex color codes for setting LED colors
                if (playerIndex == 0)
                {
                    if (ColorUtility.TryParseHtmlString("#6EC8FA", out ledColor)) // Light blue
                    {
                        gamepad.SetLightBarColor(ledColor);
                        Debug.Log("Player 1 LED color set to blue.");
                    }
                    else
                    {
                        Debug.LogError("Invalid hex code for Player 1 LED.");
                    }
                }
                else if (playerIndex == 1)
                {
                    if (ColorUtility.TryParseHtmlString("#FF007F", out ledColor)) // Pink FF007F
                    {
                        gamepad.SetLightBarColor(ledColor);
                        Debug.Log("Player 2 LED color set to pink.");
                    }
                    else
                    {
                        Debug.LogError("Invalid hex code for Player 2 LED.");
                    }
                }
            }
            else
            {
                Debug.LogWarning($"No DualSense controller found for Player {playerIndex + 1}.");
            }
        }
    }

    // Optional method to reset LED colors for all players
    public void ResetLEDColors()
    {
        Debug.Log("Resetting LED colors for all players.");
        // Reset LED colors for all connected controllers
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            SetLEDColor(i);  // Reapply the LED color based on the index
        }
    }
}
