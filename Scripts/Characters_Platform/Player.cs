using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter
{
    // TODO: IF player super dashes trough an enemy start combat with player using super dash on enemies!!!.

    private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    private float gravity;

    public PlayerData playerData;
    public InventoryObject inventory;

    [Header("Inventory")]
    [SerializeField] private InventoryManager inventoryManager;
    public int moneyAmount;

    [Header("Charms")]

    [Header("Combat Stats")]
    [SerializeField] private int maxHealth;

    [Header("Ground/Wall Check")]
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float groundCheckSize;
    [SerializeField] private float wallCheckDistance;
    private bool isTouchingCeiling;
    private bool isGrounded;
    private bool isTouchingWall;


    [Header("Jumping")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float lowJumpMultiplier;

    private bool canJump;

    private int amountOfJumpsLeft;

    [Header("Running")]
    [SerializeField] private float moveSpeed;
    private float xInput;

    [Header("Dash")]
    [SerializeField] private float dashDistance = 30f;
    [SerializeField] private float dashDuration = 0.15f;
    [SerializeField] private float dashCooldown = 0.5f;
    private float lastDashTime = 0;

    private bool canDash;
    private bool isDashing;

    [Header("SuperDash")]
    [SerializeField] private float superDashSpeed; // how fast super dash moves
    private float startTime;
    [SerializeField] private float holdDuration; // how super dash button has to be held for it to activate
    private bool isSuperDashing;
    private bool canSuperDash;

    [Header("Grapple")]
    [SerializeField] private GameObject grappleHook;

    [Header("Crouch")]
    [SerializeField] private float crouchMoveSpeedMultiplier;
    private bool canCrouch;
    private bool isCrouching;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravity = rb.gravityScale;
        grappleHook.SetActive(false);
        SetCharms();

        StaticGameData.playerMaxHealth = maxHealth;

        amountOfJumpsLeft = playerData.maxJumps;
    }


    // Update is called once per frame
    protected virtual void Update()
    {
        // Call checks!
        // TODO: Move ability checks to after input so that they arent being done constantly -> increased performance.
        CheckInput();
        CheckIfCanJump();
        CheckIfCanDash();
        CheckIfCanSuperDash();
        CheckIfCanCrouch();
        CheckIfGrappleHookIsUnlocked();

        SetAnimations();


        if (Input.GetKeyDown("o"))
        {
            SaveSystem.ClearSave();
        }

        if (Input.GetKeyDown("l"))
        {
            SaveSystem.LoadPlayer(this);
            inventory.Load();
        }

        if (Input.GetKeyDown("p"))
        {
            SaveSystem.SavePlayer(this);
            inventory.Save();
        }


        // Increment how fast player is falling as he falls
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        // If player lets go of space while jumping their jump is cut off and lowered
        else if (rb.velocity.y > 0 && !Input.GetKey("space"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        // Stop super dash if touching wall
        if (isTouchingWall) // or trying to jump to cancel it
        {
            rb.gravityScale = gravity;
            isSuperDashing = false;
        }

        // Flip player when he moves in the opposite direction and isnt dashing or super dashing.
        if (!isFacingRight && xInput < 0 && !isDashing && !isSuperDashing)
            Flip();
        if (isFacingRight && xInput > 0 && !isDashing && !isSuperDashing)
            Flip();
    }

    private void FixedUpdate()
    {
        // Move player if he isnt dashing or super dashing.
        if (!isDashing && !isSuperDashing && !isCrouching)
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);

        if (Input.GetKey("s"))
        {
            if (canCrouch)
            {
                Crouch();
            }
        }
        else if (isTouchingCeiling)
        {
            if (canCrouch)
            {
                Crouch();
            }
        }
        else if (!isTouchingCeiling)
            isCrouching = false;


        CheckEnvironment();
    }

    public void ChangeMoneyAmount(int change)
    {
        moneyAmount += change;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }


    private void SetCharms()
    {
        for (int i = 1; i < inventory.container.EquippedCharms.Count; i++)
        {
            StaticGameData.playerDamageAttributes = inventory.container.EquippedCharms[0].charm.damageAtrributes;
            inventory.container.EquippedCharms[i].charm.SecondaryUse();
        }
    }

    private void CheckInput()
    {
        

        // Input for running
        xInput = Input.GetAxisRaw("Horizontal");

        // Input for jump
        if (Input.GetKeyDown("space"))
        {
            Jump();
            anim.SetTrigger("jump");
        }

        // Input for dash
        if (Input.GetKeyDown("left shift"))
        {
            if (canDash)
            {
                StartCoroutine(Dash(facingDir));
            }
        }

        // Input for super dash
        if (Input.GetKeyDown("z"))
        {
            startTime = Time.time;
        }

        if (Input.GetKey("z"))
        {
            if (canSuperDash)
            {
                if (startTime + holdDuration <= Time.time)
                {
                    SuperDash();
                }
            }
        }

        // Input for attack
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("attack");
        }
    }

    private void SetAnimations()
    {
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);

        if (Mathf.Abs(rb.velocity.x) >= 0.1f)
        {
            anim.SetBool("isRunning", true);
        }
        else
            anim.SetBool("isRunning", false);

        if (isDashing)
        {
            anim.SetBool("isDashing", true);
        }
        else
            anim.SetBool("isDashing", false);

        if (isCrouching)
            anim.SetBool("isCrouching", true);
        else if (!isCrouching && !isTouchingCeiling)
            anim.SetBool("isCrouching", false);

        anim.SetBool("isTouchingCeiling", isTouchingCeiling);
    }

    // Logic for jumping
    private void Jump()
    {
        if (canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;

            if (isSuperDashing)
            {
                rb.gravityScale = gravity;
                isSuperDashing = false;
            }
        }
    }

    // Check if player can jump
    private void CheckIfCanJump()
    {
        // Reset jump amount if player is touching ground and not going up
        if (isGrounded && rb.velocity.y <= 3)
        {
            amountOfJumpsLeft = playerData.maxJumps;
        }
        if (amountOfJumpsLeft <= 0)
        {
            canJump = false;
        }
        else
            canJump = true;
    }

    // Logic for the dash ability
    IEnumerator Dash(int direction)
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(dashDistance * direction, 0f);
        rb.gravityScale = 0;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        rb.gravityScale = gravity;
    }
    
    // Checks if player can dash
    private void CheckIfCanDash()
    {
        if (playerData.hasGainedDash)
        {
            // Can dash is reset if player is grounded and dash cooldown has passed.
            if (isGrounded && Time.time - lastDashTime > dashCooldown)
            {
                lastDashTime = Time.time;
                canDash = true;
            }
        }
        else
            canDash = false;
    }

    // Logic for the super dash ability.
    private void SuperDash()
    {
        canSuperDash = false;
        isSuperDashing = true;
        rb.velocity = new Vector2(superDashSpeed * facingDir, 0f);
        rb.gravityScale = 0;
    }

    // Checks if player can super dash.
    private void CheckIfCanSuperDash()
    {
        if (playerData.hasGainedSuperDash)
        {
            // TODO: add more checks, ex: groundcheck, wallcheck
            canSuperDash = true;
        }
        else
            canSuperDash = false;
    }

    // Logic for crouching
    private void Crouch()
    {
        isCrouching = true;
        rb.velocity = new Vector2(xInput * moveSpeed * crouchMoveSpeedMultiplier * Time.deltaTime, rb.velocity.y);
    }

    // Checks if player can crouch
    private void CheckIfCanCrouch()
    {
        if (playerData.hasGainedCrouch)
        {
            if (isGrounded && !isDashing && !isSuperDashing)
                canCrouch = true;
        }
        else
            canCrouch = false;
    }

    // Checks if grapple hook has been unlocked and then sets it active if yes
    private void CheckIfGrappleHookIsUnlocked()
    {
        if (playerData.hasGainedGrappleHook)
        {
            grappleHook.SetActive(true);
        }
    }

    // Checks for collision between player and environment and returns a boolean.
    private void CheckEnvironment()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckSize, whatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance * facingDir, whatIsGround);
        isTouchingCeiling = Physics2D.OverlapCircle(ceilingCheck.position, groundCheckSize, whatIsGround);
    }

    
    // Draw gizmos for environment checks, to make it easier to see in unity.
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckSize);
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
        Gizmos.DrawWireSphere(ceilingCheck.position, groundCheckSize);
    }
}
