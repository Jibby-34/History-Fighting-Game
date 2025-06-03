using UnityEngine;
using System.Collections;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private CharacterData characterData;
    public HealthManager healthManager;
    public AttackHandler attackHandler;
    public BoxCollider2D hitbox;
    public BoxCollider2D playerHitbox;
    public WinManager winManager;
    public TextMeshProUGUI characterText;


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

    private bool isAttacking = false;
    int facingDirection = 1;

    public KnockbackHandler knockBackhandler;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.linearDamping = 0f;
        if (playerId == 1)
        {
            characterData = GameData.player1Character;
        }
        else
        {
            characterData = GameData.player2Character;
        }
        gameObject.transform.localScale = characterData.spriteScale;
        playerHitbox.size = characterData.hitboxScale;
        playerHitbox.offset = characterData.hitboxPosition;
        characterText.text = characterData.characterName;

        // Change controls if player 2
        if (playerId == 2)
        {
            horizontalAxis = "Horizontal 2";
            jumpAxis = "Jump 2";
            lightAttack = "Fire 2";
        }

        ApplyCharacterData(characterData);
        SetupHitboxes();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleAttack();
        UpdateAnimations();
        CheckDead();
    }

    void CheckDead()
    {
        if (healthManager.GetHealth() <= 0)
        {
            playerHitbox.enabled = false;
            winManager.FlagLoss(playerId);
        }
        if (groundCheck.transform.position.y <= -6) {
            winManager.FlagLoss(playerId);
        }
    }

    void HandleMovement()
    {
        // Cancel movement if attacking
        if (isAttacking) return;

        if (knockBackhandler.knockbackTimer < 0.05)
        {
            //rb.linearVelocity = new Vector2();
        }
        else if (knockBackhandler.knockbackTimer > 0)
        {
            knockBackhandler.knockbackTimer = knockBackhandler.knockbackTimer - Time.deltaTime;
            return;
        }

        // Move based off defined axis value (controllers will have a -1 to 1 range)
        float moveInput = Input.GetAxisRaw(horizontalAxis);

        // Pull the character's unique speed into the equation, turning it into a movement vector
        rb.linearVelocity = new Vector2(moveInput * characterData.moveSpeed, rb.linearVelocity.y);

        // Track the facing direction of the character 
        if (moveInput != 0)
        {
            facingDirection = (int)Mathf.Sign(moveInput);
            spriteRenderer.flipX = facingDirection == -1;
        }

        hitbox.transform.localPosition = new Vector2(
            characterData.lightAttack.hitboxOffset.x * facingDirection,
            characterData.lightAttack.hitboxOffset.y
        );
    }


    void HandleJump()
    {
        // Cancel jump if attacking
        if (isAttacking) return;

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

            AttackData attack = characterData.lightAttack;
            if (attack.attackAnimation != null)
                animator.Play(attack.attackAnimation.name);
            else
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
        yield return StartCoroutine(attackHandler.SlideForwardDuringAttack(characterData.lightAttack.force, characterData.lightAttack.duration, facingDirection));

        // Wait until the animation is done
        yield return new WaitForSeconds(characterData.lightAttack.duration);

        isAttacking = false;
    }

    public void EnableHitbox()
    {
        hitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.transform != transform)
        {
            HealthManager health = other.GetComponent<HealthManager>();
            KnockbackHandler knockbackHandler = other.GetComponent<KnockbackHandler>();

            if (knockbackHandler != null)
            {
                knockbackHandler.ApplyKnockback(gameObject.transform,
                    characterData.lightAttack.launchAngle,
                    characterData.lightAttack.delayTime,
                    characterData.lightAttack.knockbackForce);
            }

            if (health != null)
            {
                health.TakeDamage(characterData.lightAttack.damage);
            }
        }
    }

    private void SetupHitboxes()
    {
        hitbox.transform.localPosition = characterData.lightAttack.hitboxOffset;
        hitbox.transform.localScale = characterData.lightAttack.hitboxSize;
    }

}
