using System.Collections;
using UnityEngine;

public class EnemyProjectileSpawner : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float delayAttack = 1.0f;

    public bool stateAttack;
    public Transform spawnPoint;
    public GameObject projectile;
    private Animator enemyAnimator;

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    private IEnumerator ProjectileLoop()
    {
        stateAttack = true;
        yield return new WaitForSeconds(delayAttack);
        ProjectileSpawner();
    }

    private void ProjectileSpawner()
    {
        if (!stateAttack) return;

        enemyAnimator.SetTrigger("Attack");
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
            StartCoroutine(ProjectileLoop());
        }
    }

    public void StopAttacking()
    {
        stateAttack = false;
        StopCoroutine("ProjectileLoop");
    }
}