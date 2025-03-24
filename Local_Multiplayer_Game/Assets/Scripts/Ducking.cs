using UnityEngine;
using UnityEngine.InputSystem;

public class Ducking : MonoBehaviour
{
    [Header("Ducking Mechanic Variables")]
    [SerializeField] private bool canDuck = true;
    [SerializeField] private float duckDuration = 0.4f; //Eden: duration of the duck in seconds
    

    private Transform firstChild; //Eden: made a top half of the player that can be deactivated to look like a duck
    private bool isDucking = false;
    private float duckTimer = 0f;


    [Header("Ducking Audio Feedback")]
    [SerializeField] private AudioSource duckingSound;
    // public InputActionReference duckAction; // New Input System

    //Sibahle: Addition of duck animations for player 1 and 2
    private Animator player1Duck;
    private Animator player2Duck;


    private void Start()
    {
        //Sibahle: Referencing the animator component on each of the players to be able to access the duck animation
        player1Duck = GetComponent<Animator>();
        player2Duck = GetComponent<Animator>();

        //Dumi: Grab the reference to the audio source comp and add it if the game does not have the source at runtime:
        duckingSound = GetComponent<AudioSource>();
        if (duckingSound == null)
        {
            duckingSound = gameObject.AddComponent<AudioSource>();
            Debug.Log("Duck sound has been added dynamically.");
        }

        if (duckingSound.clip == null)
        {
            Debug.LogError("Duck sound AudioSource has no AudioClip assigned!");
        }
        //Eden: get the first child of the player so that it can be deactivated
        if (transform.childCount > 0)
        {
            firstChild = transform.GetChild(0); 
        }
        else
        {
            Debug.LogError("No children found for the player");
        }
    }

    /*private void Update()
    {
        //Eden: if c is pressed or held down, start or reset the duck timer
       if (Input.GetKey(canDuck)
        {
            if (!isDucking)
            {
                StartDucking();
            }
        }
    }*/

    /*public void Duck()
    {
        //handle ducking logic
        if (Input.GetKey(KeyCode.C) && canDuck)
        {
            if (!isDucking)
            {
                StartDucking();
            }
        }
    }*/


    public void StartDucking()
    {
        if(canDuck && !isDucking)
        {
            isDucking = true;
            canDuck = false;
            if(duckingSound !=null && duckingSound.clip != null)
            {
                duckingSound.Play();
            }
            else
            {
                Debug.LogError("the duck sound is missing an audi clip");
            }
          

            //Eden: here I deactivate the first child
            if (firstChild != null)
            {
                firstChild.gameObject.SetActive(false);
            }

            //Eden: start the timer for chosen duration
            duckTimer = duckDuration;
            Invoke("StopDucking", duckDuration); //Eden: automatically StopDucking after the duration
        }
    }

    //Sibahle: Addition of methods to trigger animations using new Input System for player 1 and player 2
    public void DuckPlayer1(InputAction.CallbackContext context)
    {
        if (isDucking)
        {
            player1Duck.SetTrigger("Player1 Duck");
            Debug.Log("Player 1 Duck Animation Success");
        }
    }

    public void DuckPlayer2(InputAction.CallbackContext context)
    {
        if (isDucking)
        {
            player2Duck.SetTrigger("Player2 Duck");
            Debug.Log("Player 2 Duck Animation Success");
        }
    }
    private void StopDucking()
    {
        //Eden: now I eactivate the first child after duration to make the ducking stop
        if (firstChild != null)
        {
            firstChild.gameObject.SetActive(true);
        }

        //Eden: now allow ducking again after the duration
        canDuck = true;
        isDucking = false;
    }

}
