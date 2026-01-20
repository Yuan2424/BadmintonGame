using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikyWall : MonoBehaviour
{
    // Start is called before the first frame update
    bool isRising;
    bool stopped;
    Rigidbody2D rb;
    float riseSpeed = 0.6f;
    float fallSpeed = 2;
    void Start()
    {
        isRising = false;
        stopped = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 && !stopped)
        {
            isRising = true;
        }
        if (isRising)
        {
            rb.velocity = Vector3.up * riseSpeed;
        }
        else if (stopped)
        {
            if (transform.position.y < -40)
            {
                rb.velocity = Vector3.zero;
            }
        }
    }
    public void stopRising()
    {
        isRising = false;
        stopped = true;
        rb.velocity = Vector3.down * fallSpeed;
    }
}
