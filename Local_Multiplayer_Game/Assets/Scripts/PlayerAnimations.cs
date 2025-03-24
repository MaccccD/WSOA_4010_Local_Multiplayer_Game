using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerAnimations : MonoBehaviour
{
    //public PlayerInputManager InputManager;

    public Animator player0Controller;
    public Animator player1Controller;

    private bool isJumping;
    private bool isDucking;
    private bool isAttacking;


   
    void Start()
    {
        player0Controller = GetComponent<Animator>();
        player1Controller = GetComponent<Animator>();
    }

    public void onJump(InputAction.CallbackContext ctx)
    {
       
    }

    public void onDuck(InputAction.CallbackContext ctx)
    {

    }

    public void onAttack(InputAction.CallbackContext ctx)
    {

    }

    void Update()
    {
       
    }
}
