using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOne : MonoBehaviour
{
    [Header("Movement Settings")] // namespaces as the libraries that hold the classes that we reference and use when programming
    [Space(20)]
    public float moveSpeedAgain = 5f;
    public float jumpForce = 7f;
    private Vector3 cubeDirection;
    private Rigidbody rb;

    [Header("Shooting Settings")]
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float projectileSpeed = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Ensure cube has a Rigidbody component
    }
    private void Start()
    {
        Debug.Log("Im running");
        if (gameObject.name == "Player_1")
        {
            GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard", Keyboard.current);
            Debug.Log(gameObject.name == "Player_1" + "has been found");
        }
    }

    public void MovePlayerOne(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log(ctx + "context has been called and is working");
            Vector2 playerInput = ctx.ReadValue<Vector2>();
            cubeDirection.x = playerInput.x;
            cubeDirection.z = playerInput.y;
        }
        else if (ctx.canceled)
        {
            cubeDirection = Vector2.zero;
        }
    }

    public void JumpPlayerOne(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && Mathf.Abs(rb.linearVelocity.y) < 0.01f) // checks the veleocity of the players rigid body
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //addn an impulse force updwards so the player can go up if the player is on the ground
        }
    }

    public void ShootPlayerOne(InputAction.CallbackContext ctx)
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


