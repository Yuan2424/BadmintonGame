using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 7.0f;
    float jumpForce = 13.0f;
    float jumpForceFromDash = 7.0f;
    float dashForce = 20.0f;
    float moveX;
    //bool jumping;
    bool dashing;
    bool grounded;
    bool hasAirJump;
    bool hasAirDash;
    int direction;
    float dashLength = 0.2f;
    float dashTime;

    //buffer variables
    float bufferTime = 0.1f;
    float jumpInputTime;
    float coyoteTime;
    float dashInputTime;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        //grounded checking

        grounded = isGrounded();
        if (grounded)
        {
            coyoteTime = Time.time;
            rb.sharedMaterial.friction = 2f;
        }
        else
        {
            rb.sharedMaterial.friction = 0;
        }

        moveX = Input.GetAxis("Horizontal") * speed;
        if (!dashing)
        {
            rb.velocity = new Vector3(moveX, rb.velocity.y);
        }

        //directions

        if (moveX > 0)
        {
            direction = 1;
        }
        else if (moveX < 0)
        {
            direction = -1;
        }

        //jumps and dashes

        if (Input.GetButtonDown("Jump"))
        {
            jumpInputTime = Time.time;
        }
        if (Time.time - jumpInputTime < bufferTime && (Time.time - coyoteTime < bufferTime || hasAirJump))
        {
            rb.sharedMaterial.friction = 0;
            jumpInputTime = -2;
            //jumping = true;
            if (Time.time - coyoteTime >= bufferTime)
            {
                hasAirJump = false;
            }
            rb.velocity = new Vector3(rb.velocity.x, 0);
            if (dashing)
            {
                rb.AddForce(Vector2.up * jumpForceFromDash, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        //&& jumping 
        {
            //jumping = false;
            rb.velocity = new Vector3(rb.velocity.x, 0.5f * rb.velocity.y);
        }
        if (Input.GetButtonDown("Shift"))
        {
            dashInputTime = Time.time;
        }
        if (Time.time - dashInputTime < bufferTime && hasAirDash)
        {
            dashInputTime = -2;
            hasAirDash = false;
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            dashing = true;
            rb.AddForce(Vector2.right * dashForce * direction, ForceMode2D.Impulse);
            dashTime = Time.time;
        }
        if (Time.time - dashTime >= dashLength)
        {
            dashing = false;
            rb.gravityScale = 3;
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
}
