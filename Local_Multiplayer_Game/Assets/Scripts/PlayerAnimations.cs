using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerAnimations : MonoBehaviour
{
    //public PlayerInputManager InputManager;

    public Animator player0Controller;
    public Animator player1Controller;
   
    void Start()
    {
        player0Controller = GetComponent<Animator>();
        player1Controller = GetComponent<Animator>();
    }

    
    void Update()
    {
        
    }
}
