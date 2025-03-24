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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpForce = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y)* jumpHeight); //Eden: Calculation to make the player jump exactly 3 unitd (jumpHeight)
        
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
