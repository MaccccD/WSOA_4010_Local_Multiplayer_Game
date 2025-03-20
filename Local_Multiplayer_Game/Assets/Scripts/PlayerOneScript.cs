using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOneScript : MonoBehaviour
{
    [Header("Movement Settings")] // Makes inspector look pretty (can also be done for text)
    public float moveSpeedAgain = 5f;
    public float jumpForce = 7f;
    private Vector3 cubeDirection;
    private Rigidbody rb;
    private Animator anim; //this

    [Header("Shooting Settings")]
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float projectileSpeed = 10f;

    private void Awake() // Difference between awake and start : awake = as soon as the game starts & start = 1st frame the game starts
    {
        rb = GetComponent<Rigidbody>(); // Ensure cube has a Rigidbody component
        anim = GetComponent<Animator>(); //"this" is referenced here
    }

    public void MovePlayerOne(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Vector2 playerInput = ctx.ReadValue<Vector2>();
            cubeDirection.x = playerInput.x;
            cubeDirection.z = playerInput.y;
        }
        else if (ctx.canceled) // "ctx" = context
        {
            cubeDirection = Vector3.zero; //resets player position/ direction
        }
    }

    public void JumpPlayerOne(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // add an impulse force upwards hence refernce of the rigidbody
        }
    }

    public void JumpPlayerTwo (InputAction.CallbackContext ctx)
    {
        if (ctx.performed && Mathf.Abs(rb.linearVelocity.y)< 0.01f)
        {
            anim.SetTrigger("Rotate");
        }
    }
    public void ShootPlayerOne(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            Rigidbody projRb = projectile.GetComponent<Rigidbody>();
            projRb.linearVelocity = transform.forward * projectileSpeed; //linear velocity of the bullet or projectile then we multiply the direction with the speed

            Destroy(projectile, 3f);// destroying bullet so that millions dont appear each time and stay
        }
    }

    private void Update()
    {
        Vector3 movement = new Vector3(cubeDirection.x, 0, cubeDirection.z) * moveSpeedAgain * Time.deltaTime;
        transform.Translate(movement);
    }
}

