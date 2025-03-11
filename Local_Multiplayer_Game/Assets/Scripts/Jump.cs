using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [Header("Jump Mechanic Details")]
    public float currentSpeed;
    [SerializeField] private bool canJump = true;
    [SerializeField] private Vector3 jumpScale = new Vector3(1f, 7f, 1f); // player jump dimensions(player will jump up to the height 3 units from the original height)
    [SerializeField] private Vector3 playerScale = new Vector3(1f, 4f, 1f); // player original dimensions with the height of 4 and the width of 1
    [SerializeField] private float originaljumpHeight = 4f;
  //  public AudioSource jumpingSound;

    
    public void JumpMech()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            currentSpeed = 0f;
            JumpingManagement(true);
            //sound update here

        }
        else if(!Input.GetKeyDown(KeyCode.Space) && !canJump) 
        {
            currentSpeed = 0.1f;
            JumpingManagement(false);

        }
    }

    private void JumpingManagement(bool isJumping)
    {
        if (isJumping)
        {
            float currentJumpHeight = playerScale.y - jumpScale.y; 
            transform.localScale = jumpScale;
            transform.position += new Vector3(0, currentJumpHeight, 0);
            canJump = false;


        }

        else
        {
            float resetHeight = jumpScale.y - playerScale.y;
            transform.localScale = playerScale;
            transform.position -= new Vector3(0, resetHeight, 0);
            canJump = true;
        }
    }
}
