using UnityEngine;
using UnityEngine.InputSystem;
public class Ducking : MonoBehaviour
{
    public float duckingSpeed;
    public float currentSpeed;
    private bool canDuck = true;
    private Vector3 duckScale = new Vector3(1f, 2f, 1f); // player duck dimensions
    private Vector3 playerScale = new Vector3(1f, 4f, 1f); // player original dimensions with the height of 4 and the width of 1
    public float duckHeight = 2f; // this value will be adjustable in the inspector based on the tested height that will work with the other melle attack mechanics hence i make it public 
    public AudioSource duckingSound;

    public void Duck()
    {
        if (Input.GetKey(KeyCode.C) && canDuck)
        {
            currentSpeed = 0f; 
            DuckingManagement(true); // so allow ducking here
            duckingSound.Play();

        }
        else if (!Input.GetKey(KeyCode.C) && !canDuck) // dont allow ducking
        {
            DuckingManagement(false);
            // dont play the sound 
        }
    }
    private void DuckingManagement(bool isDucking)
    {
        if (isDucking)
        {
            transform.localScale = duckScale;
            transform.position = new Vector3(transform.position.x, transform.position.y - duckHeight, transform.position.z); // the value of this cna be adjudtes based on the actula heght of the duck and how lpw the player can go 
            canDuck = false;
            
            
        }

        else if (!isDucking)
        {
            transform.localScale = playerScale;
            transform.position = new Vector3(transform.position.x, transform.position.y + duckHeight, transform.position.z);
            canDuck = true;
        }

    }


}
