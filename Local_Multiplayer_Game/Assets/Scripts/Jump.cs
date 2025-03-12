using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [Header("Jump Mechanic Details")]
    public float currentmoveSpeed;
    public AudioSource jumpingSound;
    [SerializeField] private bool canJump = false;
    [Header("Jumping Physics")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float groundCheckDistance = 1.1f;
    public float jumpHeight = 3f;




    private void Start()
    {
        rb = GetComponent<Rigidbody>();
      
    }

    private void Update()
    {
        CheckGroundStatus();
        JumpMech();
    }
    public void JumpMech()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            currentmoveSpeed = 0f;
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);// so adding the jump force to the player so it goes upwards by using the rigid bodies addforce
            canJump = true; //to prevent double jump here
            jumpingSound.Play();

        }
        
    }

    public void CheckGroundStatus()
    {
        if(Physics.Raycast(transform.position, Vector3.down, groundCheckDistance))
        {
            canJump = true;
        }
    }


}
