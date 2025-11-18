using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public int startingHealth;
    public int currentHealth;

    void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage()
    {
        currentHealth -= 1;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
