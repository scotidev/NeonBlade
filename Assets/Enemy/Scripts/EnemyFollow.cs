using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 directionTarget;
    private Animator enemyAnimator;
    private EnemyProjectileSpawner projectile;
    private Transform meleeHitboxTransform;

    public float speedMov = 2f;
    public float distanceTarget = 10.0f;
    public bool isMelee = false;
    public AudioClip attackSoundClip;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();

        if (!isMelee)
        {
            projectile = GetComponent<EnemyProjectileSpawner>();
        }
        else
        {
            meleeHitboxTransform = transform.Find("MeleeHitbox");
        }
    }

    void Update()
    {
        Vector3 playerTargetPos = new Vector2(GameManager.instance.getPlayerRef().transform.position.x, transform.position.y);
        float distanceToPlayer = Vector2.Distance(transform.position, playerTargetPos);

        directionTarget = (playerTargetPos - transform.position).normalized;

        bool facingLeft = directionTarget.x < 0;

        GetComponent<SpriteRenderer>().flipX = facingLeft;

        FlipMeleeHitboxScale(facingLeft);

        if (distanceToPlayer <= distanceTarget)
        {
            directionTarget = Vector2.zero;
            enemyAnimator.SetBool("isRunning", false);

            if (isMelee)
            {
                enemyAnimator.SetTrigger("Attack");
                PlayAttackSound();
            }

            if (!isMelee && projectile != null && !projectile.stateAttack)
            {
                projectile.StartCoroutine("ProjectileLoop");
                PlayAttackSound();
            }
        }
        else
        {
            enemyAnimator.SetBool("isRunning", true);

            if (!isMelee && projectile != null && projectile.stateAttack)
            {
                projectile.StopAttacking();
            }
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(directionTarget.x * speedMov, rb.linearVelocity.y);
    }

    private void PlayAttackSound()
    {
        if (SFXManager.Instance != null && attackSoundClip != null)
        {
            SFXManager.Instance.PlaySound(attackSoundClip);
        }
    }


    private void FlipMeleeHitboxScale(bool shouldFlip)
    {
        if (isMelee && meleeHitboxTransform != null)
        {
            float scaleFactor = shouldFlip ? -1f : 1f;

            Vector3 currentScale = meleeHitboxTransform.localScale;

            meleeHitboxTransform.localScale = new Vector3(scaleFactor, currentScale.y, currentScale.z);
        }
    }
}