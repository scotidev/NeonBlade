using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    private Animator enemyAnimator;

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemyAnimator.SetTrigger("isAttacking");
        }
    }
}