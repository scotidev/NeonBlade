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

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);

        if (other.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
            Destroy(other.gameObject);
        }

    }
}
