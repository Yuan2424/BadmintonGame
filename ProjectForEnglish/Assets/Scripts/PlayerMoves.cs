using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 6.0f;
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
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        grounded = isGrounded();
        moveX = Input.GetAxis("Horizontal") * speed;
        if (!dashing)
        {
            rb.velocity = new Vector3(moveX, rb.velocity.y);
        }
        if (Input.GetButtonDown("Jump") && (grounded || hasAirJump))
        {
            //jumping = true;
            if (!grounded)
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
        if (moveX > 0)
        {
            direction = 1;
        }
        else if (moveX < 0)
        {
            direction = -1;
        }
        if (Input.GetButtonDown("Shift") && hasAirDash)
        {
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
        if (rb.Cast(Vector2.down, hits, 0.25f) > 0)
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
