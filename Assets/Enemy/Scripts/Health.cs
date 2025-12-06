using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public int startingHealth;
    public int currentHealth;
    private Animator animator;

    [Header("Audio Settings")]
    public AudioClip hurtSoundClip;

    void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage()
    {
        currentHealth -= 1;
        animator.SetTrigger("Hurt");

        if (SFXManager.Instance != null && hurtSoundClip != null)
        {
            SFXManager.Instance.PlaySound(hurtSoundClip);
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
