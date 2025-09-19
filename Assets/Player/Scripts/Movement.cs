using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator playerAnimator;

    float directionMove = 0.0f;
    bool isFacingRight = true;

    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 4.0f;

    private GameInputActions playerControls;
    private InputAction move;

    private void Awake()
    {
        playerControls = new GameInputActions();
    }

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
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
        FlipPlayer();
    }

    private void FlipPlayer()
    {
        if ((isFacingRight && directionMove < 0f) || (!isFacingRight && directionMove > 0f))
        {
            isFacingRight = !isFacingRight;
            GetComponent<SpriteRenderer>().flipX = !isFacingRight;
        }

        playerAnimator.SetBool("isWalking", directionMove != 0);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(directionMove * moveSpeed, rb.linearVelocity.y);
    }
}
