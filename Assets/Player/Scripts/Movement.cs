using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;

    float directionMove = 0.0f;

    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 2.0f;

    private GameInputActions playerControls;
    private InputAction move;

    private void Awake()
    {
        playerControls = new GameInputActions();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    void Update()
    {
        directionMove = move.ReadValue<float>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(directionMove * moveSpeed, rb.linearVelocity.y);
    }
}
