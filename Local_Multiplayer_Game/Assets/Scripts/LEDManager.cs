using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class LEDManager : MonoBehaviour
{
    public static LEDManager instance; 

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
    }

    //Eden: Depending on player index change the colour of the LED on controller
    public void SetLEDColor(int playerIndex)
    {
        if (Gamepad.all.Count > playerIndex)
        {
            var gamepad = (DualSenseGamepadHID)Gamepad.all[playerIndex];

            if (gamepad != null)
            {
                Color ledColor;

                //Eden: Change colour to specific hex code
                if (playerIndex == 0)
                {
                    if (ColorUtility.TryParseHtmlString("#6EC8FA", out ledColor)) //Eden: Light blue
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
                    if (ColorUtility.TryParseHtmlString("#FF007F", out ledColor)) //Eden: Pink FF007F
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

    //Eden: Optional method to reset LED colors for all players, I tried to use this to change dynamically regardless of which controller plugged in first
    public void ResetLEDColors()
    {
        Debug.Log("Resetting LED colors for all players.");
        //Eden :Reset LED colors for all connected controllers
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            SetLEDColor(i);  //Eden: reapply the LED color based on the index
        }
    }
}
