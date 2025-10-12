using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float crouchSpeed = 2f;
    public float jumpForce = 5f;

    [Header("Dash")]
    public float dashSpeed = 12f;
    public float dashDuration = 0.2f;

    [Header("Colliders")]
    public Collider2D standingCollider;
    public Collider2D crouchCollider;
   

    private Rigidbody2D rb;
    public bool isGrounded;
    private bool isCrouching;
    private bool isDashing;
    private float horizontalInput;

    private Animator animator;

    //cosas del anim
    public bool facingRight = true;

    //aca agarro el manager de acciones
    public ManagerLight manajerLights;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        GetInput();
        HandleJump();
        HandleDash();
        HandleCrouch();
        UpdateAnimations();
        FlipSprite();


    }

    private void FixedUpdate()
    {
        HandleMovement(); 
    }

    private void FlipSprite()
    {
        
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            if (horizontalInput > 0 && !facingRight)
            {
                Flip();
            }
            else if (horizontalInput < 0 && facingRight)
            {
                Flip();
            }
        }
    }

   
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void UpdateAnimations()
    {
        
        if (animator == null) return;

        bool isWalking = Mathf.Abs(horizontalInput) > 0.1f && isGrounded && !isCrouching && !isDashing;
        animator.SetBool("walking", isWalking);


        bool isFalling = !isGrounded && rb.velocity.y < -0.5f && !isDashing;
        animator.SetBool("isFalling", isFalling);
        if (!isGrounded)
        {
            animator.SetBool("walking", false);
        }
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void HandleMovement()
    {
        if (isDashing) return;

        float currentSpeed = isCrouching ? crouchSpeed : moveSpeed;
        rb.velocity = new Vector2(horizontalInput * currentSpeed, rb.velocity.y);
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching && !isDashing)
        {
            if(manajerLights.currentFire > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                if (animator != null) animator.SetTrigger("jumping");
                manajerLights.UseFire(1);
            }

           
        }
    }
    private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && !isCrouching)
        {
            if (manajerLights.currentFire > 0)
            {
                float dashDirection = horizontalInput != 0 ? horizontalInput : (facingRight ? 1f : -1f);
                StartCoroutine(Dash(dashDirection));
                manajerLights.UseFire(1);
            }

                
        }
    }

    private void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.S) && isGrounded && !isDashing)
        {
            if (!isCrouching)
            {
                SetCrouchState(true);
            }
            else
            {
                SetCrouchState(false);
            }
        }
    }

    private void SetCrouchState(bool crouch)
    {
        isCrouching = crouch;
        standingCollider.enabled = !crouch;
        crouchCollider.enabled = crouch;

        
        if (animator != null)
            animator.SetBool("crouching", crouch);
    }

    private IEnumerator Dash(float direction)
    {
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(direction * dashSpeed, 0f);

        
        if (animator != null)
            animator.SetTrigger("dashing");

        yield return new WaitForSeconds(dashDuration);
        rb.gravityScale = originalGravity;
        isDashing = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            
           
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
            isGrounded = false;
    }
}

