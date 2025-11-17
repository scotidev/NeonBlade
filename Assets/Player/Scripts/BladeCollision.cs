using UnityEngine;

public class BladeCollision : MonoBehaviour
{
    public string targetTag;
    public float timeToAdd = 2f;
    private DeathTimer deathTimer;

    void Start()
    {
        deathTimer = FindFirstObjectByType<DeathTimer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == targetTag)
        {
            Health enemyHealth = other.GetComponent<Health>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage();

                if (enemyHealth.currentHealth <= 0 && deathTimer != null)
                {
                    deathTimer.AddTime(timeToAdd);
                }
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }
}
