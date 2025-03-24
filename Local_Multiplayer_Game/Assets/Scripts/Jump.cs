using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [Header("Jump Mechanic Details")]
    public float currentmoveSpeed;
    [SerializeField] private bool canJump = false;

    [Header("Jumping Physics")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float groundCheckDistance = 2.1f;
    public float jumpHeight = 6f;
    private float jumpForce;
    [Header("Jumping Audio Feedback")]
    [SerializeField] public AudioSource jumpingSound;

    //Sibahle: Addition of jump animations for player 1 and 2
    private Animator player1Jump;
    private Animator player2Jump;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpForce = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y)* jumpHeight); //Eden: Calculation to make the player jump exactly 3 unitd (jumpHeight)

        //Sibahle: Referencing the animator component on each of the players to be able to access the jump animation
        player1Jump = GetComponent<Animator>();
        player2Jump = GetComponent<Animator>();

        //Dumi: Grab the reference to the audio source comp and add it if the game does not have the source at runtime:
        jumpingSound = GetComponent<AudioSource>();

        if (jumpingSound == null)
        {
            jumpingSound = gameObject.AddComponent<AudioSource>();
            Debug.Log("Jump  soun has been added dynamically.");
        }

        if (jumpingSound.clip == null)
        {
            Debug.LogError("Jump sound AudioSource has no AudioClip assigned!");
        }
    }

    /*private void Update()
    {
        CheckGroundStatus();
        //JumpMech();
    }*/
    public void JumpMech()
    {
        if (canJump)
        {
            currentmoveSpeed = 0f;
            //rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);//Dumi: so adding the jump force to the player so it goes upwards by using the rigid bodies addforce
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z); //Eden: Apply the jump force by changing y velocity
            canJump = false; //Dumi: to prevent double jump here //Eden: I changed this from true to false 
            if (jumpingSound != null && jumpingSound.clip != null)
            {
                jumpingSound.Play();
            }
            else
            {
                Debug.LogError("the jump soound is not there bc there is no clip");
            }

        }

    }

    //Sibahle: Addition of methods to trigger jump animations using new Input System for player 1 and player 2
    public void JumpPlayer1(InputAction.CallbackContext context)
    {
        if (canJump)
        {
            player1Jump.SetTrigger("Idle");
        }
        else //Sibahle: otherwise if the player 1 does jump, the specific jump animation for it will trigger
        {
            player1Jump.SetTrigger("Player1 Jump");
            Debug.Log("Player 1 Jump Animation Success");
        }
    }

    public void JumpPlayer2(InputAction.CallbackContext context)
    {
        if (canJump)
        {
            player2Jump.SetTrigger("Idle2");
        }
        else //Sibahle: otherwise if the player 2 does jump, the specific jump animation for it will trigger
        {
            player2Jump.SetTrigger("Player2 Jump");
            Debug.Log("Player 2 Jump Animation Success");
        }
    }

    public void CheckGroundStatus()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundCheckDistance)) 
        {
            if (hit.collider.CompareTag("Ground")) //Eden: Detection for ground tag 
            {
                canJump = true;
                return;
            }
        }
        canJump = false; //Eden: if no ground detected, player cant jump

    }
}
