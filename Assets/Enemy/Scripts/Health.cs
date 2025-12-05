using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public int startingHealth;
    public int currentHealth;
    private Animator animator;

    void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage()
    {
        currentHealth -= 1;
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
