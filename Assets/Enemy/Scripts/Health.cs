using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public int startingHealth;
    public int currentHealth;

    [Header("iFrames Settings")]
    [SerializeField] private int iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRender;
    [SerializeField] private int[] targetLayers;

    void Awake()
    {
        currentHealth = startingHealth;
        spriteRender = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        IgnoreAllLayersCollision(false);
    }

    void Update()
    {

    }

    public void TakeDamage()
    {
        currentHealth -= 1;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(Invulnerability());
        }
    }

    private void IgnoreAllLayersCollision(bool isIgnored)
    {
        foreach (int layerNum in targetLayers)
        {
            Physics2D.IgnoreLayerCollision(gameObject.layer, layerNum, isIgnored);
        }
    }

    private IEnumerator Invulnerability()
    {
        IgnoreAllLayersCollision(true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRender.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRender.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        IgnoreAllLayersCollision(false);
    }
}
