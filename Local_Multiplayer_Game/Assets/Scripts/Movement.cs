using UnityEngine;
using UnityEngine.InputSystem;
public class Movement : MonoBehaviour
{


    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public InputAction playerControl;

    Vector2 moveDirection = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = playerControl.ReadValue<Vector2>();
        
    }
}
