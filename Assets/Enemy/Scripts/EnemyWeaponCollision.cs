using UnityEngine;

public class EnemyWeaponCollision : MonoBehaviour
{
    public float timeToSubtract = 3f;
    private DeathTimer deathTimer;

    void Start()
    {
        deathTimer = FindFirstObjectByType<DeathTimer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInvulnerability playerInvulnerability = other.GetComponent<PlayerInvulnerability>();

            if (playerInvulnerability != null)
            {
                playerInvulnerability.ActivateInvulnerability();
            }

            if (deathTimer != null)
            {
                deathTimer.SubtractTime(timeToSubtract);
            }
        }
    }
}
