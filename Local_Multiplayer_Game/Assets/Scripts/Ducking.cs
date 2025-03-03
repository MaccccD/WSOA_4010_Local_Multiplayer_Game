using UnityEngine;
using UnityEngine.InputSystem;
public class Ducking : MonoBehaviour
{
    public float duckingSpeed;
    public float currentSpeed;
    private bool canDuck = true;
    private Vector3 duckScale = new Vector3(1f, 0.5f, 1f);
    private Vector3 playerScale = new Vector3(1f, 1.5f, 1f);
    public float duckHeight = 0.5f; // this value will be adjustable in the inspector based on the tested height that will work with the other melle attack mechanics hence i make it public 


    public void Duck()
    {
        if (Input.GetKey(KeyCode.C) && canDuck)
        {
            currentSpeed = 0f; 
            DuckingManagement(true); // so allow ducking here

        }
        else if (!Input.GetKey(KeyCode.C) && !canDuck) // dont allow ducking
        {
            DuckingManagement(false);
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
