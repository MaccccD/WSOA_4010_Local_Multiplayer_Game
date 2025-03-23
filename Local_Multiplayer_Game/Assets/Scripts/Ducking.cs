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


    private void Start()
    {
        //Dumi: Grab the reference to the audio source comp and add it if the game does not have the source at runtime:
        duckingSound = GetComponent<AudioSource>();
        if (duckingSound == null)
        {
            duckingSound.gameObject.AddComponent<AudioSource>();
            Debug.Log(duckingSound + "has been added successfully(2)");
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
            duckingSound.Play();

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
