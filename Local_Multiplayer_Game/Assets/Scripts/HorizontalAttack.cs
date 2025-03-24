using UnityEditor.Timeline.Actions;
using UnityEngine;

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
    [SerializeField] public AudioSource attackSound;

    private void Start()
    {
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
