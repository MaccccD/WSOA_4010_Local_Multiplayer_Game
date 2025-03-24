using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//Eden: this script is used for players to 'ready up' and go to next screen
public class PlayerReady : MonoBehaviour
{
    private bool isReady = false; //Eden: declare bool that will track if player has pressed ready (square) in CURRENT SCENE
    private int lastSceneIndex; //EdeN: this is so we are able to ready up in each scene so keep track of which scene we were in

    //Eden: awake we record which scene player STARTS IN 
    private void Awake()
    {
        lastSceneIndex = SceneManager.GetActiveScene().buildIndex; //Eden: here we get the current build index of current scene and store as lastSceneIndex
    }

    //Eden: in update we can check if the current scene has changed
    private void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //Eden: this is the check for current scene called every fram
        if (currentSceneIndex != lastSceneIndex) //Eden:If its a diff scene then
        {
            lastSceneIndex = currentSceneIndex; //Eden: update lastSceneIndex value
            isReady = false; //Eden: because we are in a new scene we can reset the ready bool so players can press ready again in new scene
            Debug.Log($"{gameObject.name} has been reset to NOT ready for scene '{SceneManager.GetActiveScene().name}'.");
        }
    }

    //Eden: This is for new input system 
    public void OnReady(InputAction.CallbackContext context) 
    {
        if (context.performed && !isReady) //Eden: if button pressed and player is !isReady ie they have not readyed up then
        {
            isReady = true; //Eden: change the bool to true ie they have pressed button
            Debug.Log($"{gameObject.name} pressed Ready and is now ready!");

            ReadyUpSceneManager.Instance?.PlayerIsReady(gameObject.name); //Eden: this sends stuff to ReadyUpSceneManager so i can toggle diff UI (feedback)
        }
    }
}
