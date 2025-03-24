using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationManager : MonoBehaviour
{
    [Header("Animator Controllers")]
    public RuntimeAnimatorController player1Controller;
    public RuntimeAnimatorController player2Controller;

    private MeshRenderer cubeMeshRenderer;
    private SpriteRenderer spriteRenderer;

    private Animator animator;
    private int playerIndex;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("AnimationManager requires an Animator on the same GameObject.");
            return;
        }
        //Dumi:
        // Get references to MeshRenderer (Cube) and SpriteRenderer (Child)
        cubeMeshRenderer = GetComponent<MeshRenderer>();  // Cube's MeshRenderer
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // Child's SpriteRenderer

        if (cubeMeshRenderer != null)
        {
            cubeMeshRenderer.enabled = true; // D: trying not to disable the mesh 
        }
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }


        //Eden get player index
        string[] parts = transform.root.name.Split('_');
        if (parts.Length > 1 && int.TryParse(parts[1], out int idx))
            playerIndex = idx;
        else
            playerIndex = 0;

        //Eden assign the correct controller or animator
        animator.runtimeAnimatorController = (playerIndex == 0)
            ? player1Controller
            : player2Controller;
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            animator.SetTrigger("isJumping");
        }
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            animator.SetTrigger("isAttacking");
        }
    }

    public void OnDuck(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            animator.SetTrigger("isDucking");
        }
    }
}
