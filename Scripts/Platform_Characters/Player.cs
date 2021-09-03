using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter
{
    [SerializeField] LayerMask whatIsGround;

    private Rigidbody2D rb;
    private Transform tr;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform feet;

    [SerializeField] private float feetSize;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float variableJumpHeight;
    private float xInput;

    [SerializeField] private int baseJumpAmount;
    private int amountOfJumpsLeft;

    private bool isGrounded;
    private bool didLetGoOfSpace;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        amountOfJumpsLeft = baseJumpAmount;
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown("space"))
        {
            if (CheckIsGrounded() || amountOfJumpsLeft > 0)
            {
                Jump();
            }
        }
        if (!didLetGoOfSpace && !Input.GetKey("space"))
        {
            didLetGoOfSpace = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeight);
        }



        if (!isFacingRight && xInput < 0)
            Flip();
        if (isFacingRight && xInput > 0)
            Flip();

        if (Input.GetKeyDown("f"))
        {
            anim.SetTrigger("attack");
        }


        bool secondaryCheck = Physics2D.OverlapCircle(feet.position, feetSize + 0.5f, whatIsGround);

        if (!isGrounded && secondaryCheck)
        {
            amountOfJumpsLeft--;
        }

        if (CheckIsGrounded())
        {
            amountOfJumpsLeft = baseJumpAmount;
            didLetGoOfSpace = false; 
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        // When player jumps he is still touching ground and amount of jjumpps is reset and that is why there is an extra jump!!!
        amountOfJumpsLeft--;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        didLetGoOfSpace = true;
    }

    private bool CheckIsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(feet.position, feetSize, whatIsGround);
        
        return isGrounded;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(feet.position, feetSize);
        Gizmos.DrawWireSphere(feet.position, feetSize + 0.5f);
    }
}
