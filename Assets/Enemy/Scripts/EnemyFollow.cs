using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 directionTarget;
    private Animator enemyAnim;

    public float speedMov = 2f;
    public float distanceTarget = 10.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 playerTargetPos = new Vector2(GameManager.instance.getPlayerRef().transform.position.x, transform.position.y);
        directionTarget = playerTargetPos - transform.position.normalized;

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
            enemyAnim.SetBool("isRunning", false);

        }
        else
        {
            enemyAnim.SetBool("isRunning", true);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(directionTarget.x * speedMov, rb.linearVelocity.y);
    }
}
