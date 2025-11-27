using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 directionTarget;
    private Animator enemyAnimator;
    private EnemyProjectileSpawner projectile;

    public float speedMov = 2f;
    public float distanceTarget = 10.0f;
    public bool isMelee = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();

        if (!isMelee)
        {
            projectile = GetComponent<EnemyProjectileSpawner>();
        }
    }

    void Update()
    {
        Vector3 playerTargetPos = new Vector2(GameManager.instance.getPlayerRef().transform.position.x, transform.position.y);
        directionTarget = playerTargetPos - transform.position;

        if (directionTarget.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Vector2.Distance(transform.position, playerTargetPos) <= distanceTarget)
        {
            directionTarget = Vector2.zero;
            enemyAnimator.SetBool("isRunning", false);

            if (!isMelee && projectile != null && !projectile.stateAttack)
            {
                projectile.StartCoroutine("ProjectileLoop");
            }
        }
        else
        {
            enemyAnimator.SetBool("isRunning", true);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(directionTarget.x * speedMov, rb.linearVelocity.y);
    }
}
