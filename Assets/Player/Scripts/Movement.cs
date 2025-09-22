using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator playerAnimator;

    private GameInputActions playerControls;
    private InputAction move;
    private InputAction jump;

    float directionMove = 0.0f;
    bool isFacingRight = true;

    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 4.0f;

    [Header("Jump Strength")]
    [SerializeField] float jumpStrength = 5.0f;
    private bool isJumping = false;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float feetRadius;
    private bool isGrounded = true;

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

        jump = playerControls.Player.Jump;
        jump.Enable();
        jump.performed += JumpPlayer;
    }

    private void OnDisable()
    {
        move.Disable();
    }

    void Update()
    {
        directionMove = move.ReadValue<float>();
        FlipPlayer();
        isGround();
    }

    private void JumpPlayer(InputAction.CallbackContext context)
    {
        if (isGrounded || !isJumping)
        {
            rb.linearVelocity = Vector2.up * jumpStrength;
            isJumping = true;
        }
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

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(directionMove * moveSpeed, rb.linearVelocity.y);
    }

    public bool isGround()
    {
        isGrounded = false;

        if (Physics2D.OverlapCircle(feetPos.position, feetRadius, groundLayer))
        {
            isGrounded = true;
        }

        return isGrounded;
    }
}
