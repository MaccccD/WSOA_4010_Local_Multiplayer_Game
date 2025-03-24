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
    public AudioSource jumpingSound;

    //Sibahle: Addition of jump animations for player 1 and 2
    private Animator animator;// animator component for both players
    private PlayerInput playerInput;//this is for the system to know which player is performing which input so that it triggers the particular animation 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpForce = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight); //Eden: Calculation to make the player jump exactly 3 unitd (jumpHeight)

        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();

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
            if(jumpingSound != null && jumpingSound.clip != null)
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
        if (context.performed && canJump)
        {
            player1Jump.SetTrigger("Player1 Jump"); //Referencing 
            Debug.Log("Player 1 is Jumping");

            JumpMech();
        }
    }

    public void JumpPlayer2(InputAction.CallbackContext context)
    {
        if (context.performed && canJump)
        {
            player2Jump.SetTrigger("Player2 Jump");
            Debug.Log("Player 2 is Jumping");

            JumpMech();
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
