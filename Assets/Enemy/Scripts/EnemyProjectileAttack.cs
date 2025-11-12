using System.Collections;
using UnityEngine;

public class EnemyProjectileAttack : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private bool disableAttack;

    public bool stateAttack;
    [SerializeField] private float delayAttack = 1.0f;

    public Transform spawnPoint;
    public GameObject projectile;
    private Animator enemyAnimator;

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    private IEnumerator timerAttack()
    {
        stateAttack = true;
        yield return new WaitForSeconds(delayAttack);
        attackProjectile();
    }

    private void attackProjectile()
    {
        enemyAnimator.SetTrigger("inAttack");

        int projectileDirection = 1;
        float spawnPointX = Mathf.Abs(spawnPoint.localPosition.x);

        if (GetComponent<SpriteRenderer>().flipX)
        {
            projectileDirection = -1;
            spawnPointX = -spawnPointX;
        }

        spawnPoint.localPosition = new Vector2(spawnPointX, spawnPoint.localPosition.y);

        GameObject projectileInstance = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);

        projectileInstance.transform.right *= projectileDirection;

        if (stateAttack)
        {
            StartCoroutine(timerAttack());
        }
    }
}
