using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuttleMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float shuttleForce = 4;
    Vector3 dest;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Vector3.Distance(transform.position, dest) < 2f)
        //{
            //rb.gravityScale = 0.5f;
        //}
    }
    public void moveShuttle(Vector3 destination)
    {
        dest = destination;
        rb.gravityScale = 0;
        rb.AddForce((destination - transform.position) * shuttleForce, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CrosshairPos") && rb.gravityScale == 0)
        {
            rb.gravityScale = 0.5f;
            rb.velocity = rb.velocity * 0.5f;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            rb.gravityScale = 0.5f;
        }
    }
}
