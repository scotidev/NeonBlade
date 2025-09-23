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

    [Header("Jump Settings")]
    [SerializeField] float jumpStrength = 5.0f;
    private bool isJumping = false;
    private float jumpTimeCounter;
    public float jumpTime = 0.3f;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float feetRadius;
    private bool isGrounded = true;

    private void Awake()
    {
        playerControls = new GameInputActions();
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.instance.setPlayerRef(gameObject);
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
        jump.Disable();
    }

    private void Update()
    {
        directionMove = move.ReadValue<float>();
        FlipPlayer();
        CheckIsGrounded();
        UpdateJump();
        UpdateAnimator();
    }

    private void JumpPlayer(InputAction.CallbackContext context)
    {
        // Apenas permite o pulo se o jogador estiver no chão.
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
        if ((isFacingRight && directionMove < 0f) || (!isFacingRight && directionMove > 0f))
        {
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(directionMove * moveSpeed, rb.linearVelocity.y);
    }

    private void CheckIsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, feetRadius, groundLayer);
    }

    private void UpdateAnimator()
    {
        playerAnimator.SetBool("isWalking", directionMove != 0);
        playerAnimator.SetBool("isJumping", !isGrounded);
    }
}