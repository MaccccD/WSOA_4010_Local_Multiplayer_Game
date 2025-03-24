using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationManager : MonoBehaviour
{
    [Header("Animator Controllers")]
    public RuntimeAnimatorController player1Controller;
    public RuntimeAnimatorController player2Controller;

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
        animator.SetBool("isJumping", ctx.performed);
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        animator.SetBool("isAttacking", ctx.performed);
    }

    public void OnDuck(InputAction.CallbackContext ctx)
    {
        animator.SetBool("isDucking", ctx.performed);
    }
}
