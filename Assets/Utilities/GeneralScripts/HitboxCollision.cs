using UnityEngine;

public class HitboxCollision : MonoBehaviour
{
    public string targetTag;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == targetTag)
        {
            Destroy(other.gameObject);
        }
    }
}
