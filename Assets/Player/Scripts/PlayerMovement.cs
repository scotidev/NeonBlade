using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private GameInputActions playerControls;
    private InputAction move;
    private InputAction jump;
    float directionMove = 0.0f;
    bool isFacingRight = true;

    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 4.0f;

    [Header("Jump Settings")]
    [SerializeField] float jumpStrength = 5.0f;
    private bool isJumping = false;
    private float jumpTimeCounter;
    public float jumpTime = 0.3f;

    [Header("Ground Check")]
    [SerializeField] private Transform feetPos;
    [SerializeField] private float feetRadius;
    public LayerMask groundLayer;
    private bool isGrounded = true;

    private void Awake()
    {
        playerControls = new GameInputActions();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.instance.setPlayerRef(gameObject);
    }

    private void Update()
    {
        directionMove = move.ReadValue<float>();
        FlipPlayer();
        isGround();
        UpdateJump();
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(directionMove * moveSpeed, rb.linearVelocity.y);
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        jump = playerControls.Player.Jump;
        jump.Enable();
        jump.performed += PlayerJump;
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }

    private void PlayerJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rb.linearVelocity = Vector2.up * jumpStrength;
            isJumping = true;
            jumpTimeCounter = jumpTime;
        }
    }

    private void UpdateJump()
    {
        if (jump.IsPressed() && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpStrength);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (jump.WasReleasedThisFrame())
        {
            isJumping = false;
        }
    }

    private void FlipPlayer()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if ((isFacingRight && directionMove < 0f) || (!isFacingRight && directionMove > 0f))
        {
            isFacingRight = !isFacingRight;

            sr.flipX = !isFacingRight;
        }
    }

    private void isGround()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, feetRadius, groundLayer);
    }

    private void UpdateAnimator()
    {
        animator.SetBool("isRunning", directionMove != 0);
        animator.SetBool("isJumping", !isGrounded);
    }
}