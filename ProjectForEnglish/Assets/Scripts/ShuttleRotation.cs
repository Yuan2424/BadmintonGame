using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuttleRotation : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 dirToMouse;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirToMouse = Input.mousePosition - transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan(dirToMouse.y / dirToMouse.x));
    }
}
