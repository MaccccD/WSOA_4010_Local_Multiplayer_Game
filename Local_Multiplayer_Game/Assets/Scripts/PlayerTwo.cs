using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTwo : MonoBehaviour
{
    [Header("Movement Settings")] // namespaces as the libraries that hold the classes that we reference and use when programming
    [Space(20)]
    public float moveSpeedAgain = 5f;
    public float jumpForce = 7f;
    private Vector3 cubeDirection;
    private Rigidbody rb;
    private Animator anim;

    [Header("Shooting Settings")]
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float projectileSpeed = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Ensure cube has a Rigidbody component
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        if(gameObject.name == "Player_2")
        {
            GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard02", Keyboard.current);
        }
    }
    public void MovePlayerTwo(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Vector2 playerInput = ctx.ReadValue<Vector2>();
            cubeDirection.x = playerInput.x;
            cubeDirection.z = playerInput.y;
        }
        else if (ctx.canceled)
        {
            cubeDirection = Vector2.zero;
        }
    }

    public void JumpPlayerTwo(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && Mathf.Abs(rb.linearVelocity.y) < 0.01f) // checks the veleocity of the players rigid body
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //addn an impulse force updwards so the player can go up if the player is on the ground
            anim.SetTrigger("Jump"); // this (calling it in the context of the new input system )
        }
    }

    public void ShootPlayerTwo(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            Rigidbody projRb = projectile.GetComponent<Rigidbody>();
            projRb.linearVelocity = transform.forward * projectileSpeed; // shoot the bullets projectile forarwrd along with the projectile speed 

            Destroy(projectile, 3f);
        }
    }

    private void Update()
    {
        Vector3 movement = new Vector3(cubeDirection.x, 0, cubeDirection.z) * moveSpeedAgain * Time.deltaTime; //fowards and backwards movement
        transform.Translate(movement);
    }
}
