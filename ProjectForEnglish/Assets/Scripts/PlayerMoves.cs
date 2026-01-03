using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator anim;
    float speed = 7.0f;
    float jumpForce = 13.0f;
    float jumpForceFromDash = 7.0f;
    float dashForce = 20.0f;
    float moveX;
    GameObject shuttle;
    GameObject crosshair;
    //bool jumping;

    //state variables
    bool dashing;
    bool dashJumping;
    bool grounded;
    bool hasAirJump;
    bool hasAirDash;
    bool timeFrozen = false;

    int direction;
    int dashDirection;
    float dashLength = 0.2f;
    float dashJumpLength = 0.5f;

    //buffer variables
    float bufferTime = 0.1f;
    float jumpInputTime;
    float coyoteTime;
    float dashInputTime;
    float dashTime;
    float dashJumpTime;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        shuttle = GameObject.Find("Shuttle");
        crosshair = GameObject.Find("Crosshair");
        Time.timeScale = 1;
    }

    
    void Update()
    {
        // Grounded checking
        grounded = isGrounded() && !dashing && !dashJumping;
        if (grounded)
        {
            coyoteTime = Time.time;
            rb.sharedMaterial.friction = 2f;
            dashJumping = false;
            dashJumpLength = 0.5f;
        }
        else
        {
            rb.sharedMaterial.friction = 0;
        }

        // Dashing checking
        if (dashing)
        {
            rb.sharedMaterial.friction = 0;
        }

        // Horizontal movement

        moveX = Input.GetAxis("Horizontal") * speed;
        if (!dashing && !dashJumping)
        {
            rb.velocity = new Vector3(moveX, rb.velocity.y);
        }
        if (!dashing && dashJumping)
        {
            rb.velocity = new Vector3(moveX + dashDirection * speed * (1 - 2 * (Time.time - dashJumpTime)), rb.velocity.y);
        }
        if (direction != dashDirection)
        {
            dashJumpLength -= 0.02f;
        }
        if (Time.time - dashJumpTime > dashJumpLength)
        {
            dashJumping = false;
            dashJumpLength = 0.5f;
        }

        // Directions

        if (moveX > 0)
        {
            direction = 1;
            spriteRenderer.flipX = false;
        }
        else if (moveX < 0)
        {
            direction = -1;
            spriteRenderer.flipX = true;
        }

        // Jumping

        if (Input.GetButtonDown("Jump") && !timeFrozen)
        {
            jumpInputTime = Time.time;
        }
        if (Time.time - jumpInputTime < bufferTime && (Time.time - coyoteTime < bufferTime || hasAirJump))
        {
            jump();
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0.5f * rb.velocity.y);
        }

        // Dashing

        if (Input.GetButtonDown("Shift") && !timeFrozen)
        {
            dashInputTime = Time.time;
        }
        if (Time.time - dashInputTime < bufferTime && hasAirDash)
        {
            dash();
        }
        if (Time.time - dashTime >= dashLength)
        {
            rb.sharedMaterial.friction = 2;
            dashing = false;
            anim.SetBool("Dashing", false);
            rb.gravityScale = 3;
        }

        // Shuttle time unfreezing

        if (Input.GetButtonDown("Z") && timeFrozen && Vector3.Distance(crosshair.transform.position, transform.position) > 2)
        {
            timeFrozen = false;
            Time.timeScale = 1;
            rb.velocity = Vector2.zero;
            shuttle.GetComponent<ShuttleMovement>().moveShuttle(crosshair.transform.position);
        }
        if (Vector3.Distance(crosshair.transform.position, transform.position) <= 2)
        {
            Color newColor = Color.red;
            newColor.a = 0.67f;
            crosshair.GetComponent<SpriteRenderer>().color = newColor;
        }
        else
        {
            Color newColor = Color.white;
            newColor.a = 0.67f;
            crosshair.GetComponent<SpriteRenderer>().color = newColor;
        }

        // Running animation
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }

        // Falling animation
        if (grounded || rb.velocity.y >= 0)
        {
            anim.SetBool("Falling", false);
        }
        else
        {
            anim.SetBool("Falling", true);
        }
    }
    bool isGrounded()
    {
        //jumping = false;
        List<RaycastHit2D> hits = new();
        if (rb.Cast(Vector2.down, hits, 0.1f) > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.CompareTag("Floor"))
                {
                    hasAirJump = true;
                    hasAirDash = true;
                    return (true);
                }
            }
        }
        return (false);
    }
    void jump()
    {
        rb.sharedMaterial.friction = 0;
        jumpInputTime = -2;
        anim.SetBool("Dashing", false);
        anim.SetTrigger("Jumping");
        if (Time.time - coyoteTime >= bufferTime)
        {
            hasAirJump = false;
        }
        rb.velocity = new Vector3(rb.velocity.x, 0);
        if (dashing)
        {
            rb.AddForce(Vector2.up * jumpForceFromDash, ForceMode2D.Impulse);
            dashJumping = true;
            dashJumpTime = Time.time;
            dashing = false;
            rb.sharedMaterial.friction = 0;
        }
        else
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    void dash()
    {
        rb.sharedMaterial.friction = 0;
        dashInputTime = -2;
        hasAirDash = false;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        dashing = true;
        anim.SetBool("Dashing", true);
        rb.AddForce(Vector2.right * dashForce * direction, ForceMode2D.Impulse);
        dashDirection = direction;
        dashTime = Time.time;
    }
    public Vector3 getPlayerVelocity()
    {
        return rb.velocity;
    }
    public Vector3 getPlayerPos()
    {
        return transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInParent<ShuttleRotation>())
        {
            Time.timeScale = 0;
            timeFrozen = true;
            crosshair.transform.position = transform.position;
        }
        if (collision.gameObject.CompareTag("Exclaim"))
        {
            collision.gameObject.GetComponent<ButtonBehavior>().activated();
        }
    }
}
