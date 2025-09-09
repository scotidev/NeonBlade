using UnityEngine;

public class Movement : MonoBehaviour
{
    float movementDirection = 0.0f;
    private Rigidbody2D rb;

    [Header("Movement Settings")]
    [SerializeField]
    float movementSpeed = 2.0f;
    void FixedUpdate()
    {

    }

    void Awake()
    {

    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Pega as entradas de movimento horizontal (teclas A/D ou setas esquerda/direita).
        float horizontalInput = Input.GetAxis("Horizontal");

        // Cria um vetor de movimento baseado na entrada do teclado.
        Vector2 movement = new Vector2(horizontalInput, 0);

        // Define a velocidade do Rigidbody2D.
        rb.linearVelocity = movement * movementSpeed;
    }
}
