using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairMove : MonoBehaviour
{
    float speed = 10;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            transform.position = transform.position + new Vector3(Input.GetAxisRaw("Horizontal") * Time.unscaledDeltaTime * speed, Input.GetAxisRaw("Vertical") * Time.unscaledDeltaTime * speed);
            Physics.SyncTransforms();
        }
        spriteRenderer.enabled = Time.timeScale == 0;
    }
}
