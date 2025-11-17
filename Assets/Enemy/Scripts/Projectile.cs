using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 3.0f;
    public float projectileSpeed = 1.0f;

    void Awake()
    {
        Destroy(gameObject, lifetime);
    }

    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = transform.right * projectileSpeed;
    }
}
