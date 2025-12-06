using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    private GameInputActions playerControls;
    private InputAction attack;
    private Animator playerAnimator;

    [Header("Attack Settings")]
    private bool isAttacking = false;
    public float attackDuration = .3f;
    public GameObject hitboxAttack;
    private Vector2 initPosHitboxAttack;

    void Awake()
    {
        playerControls = new GameInputActions();
    }

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        initPosHitboxAttack = hitboxAttack.transform.localPosition;
    }

    private void OnEnable()
    {
        attack = playerControls.Player.Attack;
        attack.Enable();
        attack.performed += onAttack;
    }

    private void OnDisable()
    {
        attack.Disable();
    }

    void onAttack(InputAction.CallbackContext context)
    {
        if (!isAttacking)
        {
            if (SFXManager.Instance != null && SFXManager.Instance.attackClip != null)
            {
                SFXManager.Instance.PlaySound(SFXManager.Instance.attackClip);
            }

            Vector2 posHitbox = initPosHitboxAttack;

            if (GetComponent<SpriteRenderer>().flipX)
            {
                posHitbox = new Vector2(-initPosHitboxAttack.x, initPosHitboxAttack.y);
            }
            else
            {
                posHitbox = new Vector2(initPosHitboxAttack.x, initPosHitboxAttack.y);
            }

            hitboxAttack.transform.localPosition = posHitbox;

            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        playerAnimator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
        playerAnimator.SetBool("isAttacking", false);
    }
}
