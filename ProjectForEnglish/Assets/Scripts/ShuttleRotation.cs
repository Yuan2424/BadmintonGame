using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuttleRotation : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!rb.velocity.Equals(Vector2.zero))
        {
            if (rb.velocity.x >= 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 45 + Mathf.Rad2Deg * Mathf.Asin(rb.velocity.y / rb.velocity.magnitude));
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 225 + Mathf.Rad2Deg * Mathf.Atan(rb.velocity.y / rb.velocity.x));
            }
        }
    }
}
