using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class HorizontalAttack : MonoBehaviour
{
    [Header("Horizontal Attack Variables")]
    [SerializeField] private float attackDuration = 0.3f;
    [SerializeField] private float attackCooldown = 0.9f;
    private Transform secondChild;
    public bool isAttacking = false;
    public bool canAttack = true;
    public float attackTimer = 0f;
    public float cooldownTimer = 0f;
    [Header(" Attack Audio Feedback")]
    [SerializeField] private AudioSource attackSound;

    //Sibahle: Addition of attack animations for player 1 and 2
    private Animator player1Attack;
    private Animator player2Attack;

    private void Start()
    {
        //Sibahle: Referencing the animator component on each of the players to be able to access the attack animation
        player1Attack = GetComponent<Animator>();
        player2Attack = GetComponent<Animator>();

        //Dumi: Grab the reference to the audio source comp and add it if the game does not have the source at runtime:
        attackSound = GetComponent<AudioSource>();
        if (attackSound == null)
        {
            attackSound = gameObject.AddComponent<AudioSource>();
            Debug.Log("attack sound has  added dynamically.");
        }

        if (attackSound.clip == null)
        {
            Debug.LogError("attack sound AudioSource has no AudioClip assigned!");
        }
        if (transform.childCount > 1)
        {
            secondChild = transform.GetChild(1);
        }
        else
        {
            Debug.LogError("Second child not found for the player!");
        }
    }


    /*private void Update()
    {

        if (!canAttack)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                canAttack = true;
            }
        }
    }*/

    public void StartAttack()
    {
        if (canAttack && !isAttacking)
        {

            isAttacking = true;
            canAttack = false;
            cooldownTimer = attackCooldown;
            if(attackSound != null & attackSound.clip != null)
            {
                attackSound.Play();
            }
            else
            {
                Debug.LogError("there is no attack sound bc the clip is not there");
            }

            if (secondChild != null)
            {
                secondChild.gameObject.SetActive(true);
            }

            attackTimer = attackDuration;
            Invoke("StopAttack", attackDuration);
        }
    }

    //Sibahle: Addition of methods to trigger animations using new Input System for player 1 and player 2
    public void AttackPlayer1(InputAction.CallbackContext context)
    {
        if (isAttacking)
        {
            player1Attack.SetTrigger("Player1 Attack");
            Debug.Log("Player 1 Attack Animation Success");
        }
        else if (!isAttacking)
        {
            player1Attack.SetTrigger("Idle");
        }
    }

    public void AttackPlayer2(InputAction.CallbackContext context)
    {
        if (isAttacking)
        {
            player2Attack.SetTrigger("Player2 Attack");
            Debug.Log("Player 2 Attack Animation Success");
        }
        else if (!isAttacking)
        {
            player2Attack.SetTrigger("Idle2");
        }
    }
    private void StopAttack()
    {
        if (secondChild != null)
        {
            secondChild.gameObject.SetActive(false);
            attackSound.Pause();
        }

        isAttacking = false;
    }
    private void Update()
    {
        if (!canAttack)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                canAttack = true;
            }
        }
    }
}
