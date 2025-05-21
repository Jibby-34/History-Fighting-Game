using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    // ScriptableObject containing control and visual info
    public CharacterData characterData;
    public AttackHandler attackHandler;

    // Double jump avoidance
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    private bool isGrounded;

    // Physics and animation
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Control config
    public int playerId = 1;
    private string horizontalAxis = "Horizontal 1";
    private string jumpAxis = "Jump 1";
    private string lightAttack = "Fire 1";
    private float moveInput = 0;
    private bool isAttacking = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Change controls if player 2
        if (playerId == 2)
        {
            horizontalAxis = "Horizontal 2";
            jumpAxis = "Jump 2";
            lightAttack = "Fire 2";
        }

        ApplyCharacterData(characterData);
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleAttack();
        UpdateAnimations();
    }

    void HandleMovement()
    {
        // Move based off defined axis value (controllers will have a -1 to 1 range)
        moveInput = Input.GetAxisRaw(horizontalAxis);

        // Pull the character's unique speed into the equation, turning it into a movement vector
        rb.linearVelocity = new Vector2(moveInput * characterData.moveSpeed, rb.linearVelocity.y);

        // Apply the vector
        if (moveInput != 0 && !isAttacking)
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1f, 1f);
    }

    void HandleJump()
    {
        // Do a check in a circular motion from an invisible block at the chracter's feet
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // If check passes, apply a vertical vector based off the character's unique jump force
        if (Input.GetButtonDown(jumpAxis) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, characterData.jumpForce);
            animator.SetBool("isJumping", true);
        }
    }

    void HandleAttack()
    {
        if (Input.GetButtonDown(lightAttack) && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("attackTrigger");
            StartCoroutine(PerformAttack());
        }
    }

    // Adds compatability for running and jumping animations
    // This provides a boolean for the animation controller
    void UpdateAnimations()
    {
        float horizontalSpeed = Mathf.Abs(rb.linearVelocity.x);

        animator.SetBool("isRunning", horizontalSpeed > 0.1f);

        if (isGrounded && rb.linearVelocity.y <= 0)
        {
            animator.SetBool("isJumping", false);
        }
    }

    // Changes player sprites to the ones specifed in the character's ScriptableObject
    public void ApplyCharacterData(CharacterData data)
    {
        if (animator != null && data.animator != null)
            animator.runtimeAnimatorController = data.animator;

        if (spriteRenderer != null && data.idleSprite != null)
            spriteRenderer.sprite = data.idleSprite;
    }
    
    private IEnumerator PerformAttack()
    {
        yield return StartCoroutine(attackHandler.SlideForwardDuringAttack(characterData.lightAttackForce, characterData.lightAttackDuration));

        // Wait until the animation is done
        yield return new WaitForSeconds(characterData.lightAttackDuration);

        isAttacking = false;
    }

}
