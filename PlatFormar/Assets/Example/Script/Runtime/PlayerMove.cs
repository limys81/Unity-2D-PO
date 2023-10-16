using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Damageable))]

/* 플레이어 */
public class PlayerMove : MonoBehaviour
{
    #region 변수
    public Animator anim;
    public Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trail;

    Damageable damageable;

    [Header("Player Move Info")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float dashingPower;
    [SerializeField] private float dasingTime;
    [SerializeField] private float dashingCoolDown;

    private bool canMove = true;

    private bool isRunning;
    private bool canDoubleJump;
    private bool canWallSlide;
    private bool isWallSliding;
    private bool canDash;
    private bool isDashing;

    private bool facingRight = true;
    private float movingInput;
    private int facingDirection = 1;
    [SerializeField] private Vector2 wallJumpDirection;

    [Header("Collision Info")]
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;

    private bool isGrounded;
    private bool isWallDetected;

    #endregion // 변수

    public bool CanMove { get
        {
            return anim.GetBool("canMove");
        }
    }

    public bool IsAlive { get
        {
            return anim.GetBool("isAlive");
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        damageable = GetComponent<Damageable>();
    }

    private void Update()
    {
        CheckInput();

        CollisionCheck();
        FlipController();
        AnimatorController();

        if (isGrounded)
        {
            canMove = true;
            canDoubleJump = true;
            canDash = true;
        }

        if (canWallSlide)
        {
            isWallSliding = true;
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * 0.1f); // Wall Slide 속도
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
            canMove = false;
        }
        
        Move();
    }

    private void CheckInput()
    {
        movingInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetAxis("Vertical") < 0)
            canWallSlide = false;

        if (Input.GetKeyDown(KeyCode.Space))
            JumpButton();
    }

    private void Move()
    {
        if (canMove && IsAlive && !isDashing && !damageable.LockVelocity)
        {
            rigid.velocity = new Vector2(movingInput * speed, rigid.velocity.y);
        }
    }

    private void JumpButton()
    {
        if (isWallSliding)
        {
            WallJump();
        }
        else if (isGrounded)
        {
            Jump();
        }
        else if (canDoubleJump)
        {
            canMove = true;
            canDoubleJump = false;
            Jump();
        }
        canWallSlide = false;
    }

    private void Jump()
    {
        if(IsAlive)
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
    }

    private void WallJump()
    {
        canMove = false;
        canDoubleJump = true;

        rigid.velocity = new Vector2(wallJumpDirection.x * -facingDirection, wallJumpDirection.y);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float origiinalGravity = rigid.gravityScale;
        rigid.gravityScale = 0f;
        rigid.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
     
        //trail.emitting = true;
        yield return new WaitForSeconds(dasingTime);
        //trail.emitting = false;

        rigid.gravityScale = origiinalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCoolDown);

        canDash = true;
    }

    private void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if (isGrounded && isWallDetected && IsAlive)
        {
            if (facingRight && movingInput < 0)
                Flip();
            else if (!facingRight && movingInput > 0)
                Flip();
        }

        if (rigid.velocity.x > 0 && !facingRight)
            Flip();
        else if (rigid.velocity.x < 0 && facingRight)
            Flip();
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rigid.velocity = new Vector2(knockback.x, rigid.velocity.y + knockback.y);
    }

    private void AnimatorController()
    {
        bool isRunning = rigid.velocity.x != 0;

        anim.SetFloat("yVelocity", rigid.velocity.y);
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isWallSliding", isWallSliding);
        anim.SetBool("isWallDitected", isWallDetected);
        anim.SetBool("isDashing", isDashing);
    }
     
    private void CollisionCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, whatIsGround);
        
        if (isWallDetected && rigid.velocity.y < 0)
            canWallSlide = true;

        if (!isWallDetected)
        {
            canWallSlide = false;
            isWallSliding = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y,
                                                        wallCheck.position.z));
    }
}
