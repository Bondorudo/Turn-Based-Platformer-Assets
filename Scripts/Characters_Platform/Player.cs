using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter
{

    private Rigidbody2D rb;
    [SerializeField] private Animator anim;


    [Header("Ground Check")]
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] private Transform feet;
    [SerializeField] private float feetSize;
    private bool isGrounded;


    [Header("Jumping")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float lowJumpMultiplier;

    private bool canJump;

    static public int baseJumpAmount = 1;
    private int amountOfJumpsLeft;


    [Header("Running")]
    [SerializeField] private float moveSpeed;
    private float xInput;


    [Header("Dash")]
    [SerializeField] private float dashDistance = 30f;
    [SerializeField] private float dashDuration = 0.15f;
    [SerializeField] private float dashCooldown = 0.5f;
    private float lastDashTime = 0;

    static public bool isDashUnlocked;
    private bool canDash;
    private bool isDashing;

    [Header("SuperDash")]
    [SerializeField] private float superDashSpeed; // how fast super dash moves
    [SerializeField] private float startUpDuration; // how super dash button has to be held for it to activate
    private bool isSuperDashing;
    private bool canSuperDash;
    static public bool isSuperDashUnlocked;

    [Header("Grapple")]
    static public bool isGrappleUnlocked;

    [Header("WallJump")]
    static public bool isWallJumpUnlocked;

    [Header("Slide/Crouch/Crawl")]
    static public bool isCrawlUnlocked;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        amountOfJumpsLeft = baseJumpAmount;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CheckInput();
        CheckIfCanJump();
        CheckIfCanDash();

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey("space"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (xInput != 0)
        {
            anim.SetTrigger("run");
        }


        if (!isFacingRight && xInput < 0)
            Flip();
        if (isFacingRight && xInput > 0)
            Flip();
    }

    private void FixedUpdate()
    {
        if (!isDashing)
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);


        CheckEnvironment();
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown("space"))
        {
            Jump();
        }

        if (Input.GetKeyDown("left shift"))
        {
            if (canDash)
            {
                StartCoroutine(Dash(facingDir));
            }
        }

        if (Input.GetKeyDown("f"))
        {
            anim.SetTrigger("attack");
        }
    }


    private void Jump()
    {
        if (canJump)
        {
            // When player jumps he is still touching ground and amount of jjumpps is reset and that is why there is an extra jump!!!
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        }
    }

    private void CheckIfCanJump()
    {
        if (isGrounded && rb.velocity.y <= 3)
        {
            amountOfJumpsLeft = baseJumpAmount;
        }
        if (amountOfJumpsLeft <= 0)
        {
            canJump = false;
        }
        else
            canJump = true;
    }

    IEnumerator Dash(int direction)
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(dashDistance * direction, 0f);
        float gravity = rb.gravityScale;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        rb.gravityScale = gravity;
    }
    
    private void CheckIfCanDash()
    {
        if (isDashUnlocked)
        {
            if (isGrounded && Time.time - lastDashTime > dashCooldown)
            {
                lastDashTime = Time.time;
                canDash = true;
            }
        }
        else
            canDash = false;
    }

    private void CheckEnvironment()
    {
        isGrounded = Physics2D.OverlapCircle(feet.position, feetSize, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(feet.position, feetSize);
        Gizmos.DrawWireSphere(feet.position, feetSize + 0.5f);
    }
}
