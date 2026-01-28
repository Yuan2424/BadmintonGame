using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Animator anim;
    SpriteRenderer spriteRenderer;
    Collider2D col;
    float crumbleDuration = 1.333f; //1.333f
    float crumbleStart;
    bool isCrumbling = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 1;
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCrumbling && Time.time >= crumbleDuration + crumbleStart)
        {
            spriteRenderer.enabled = false;
            col.enabled = false;
        }
        if (Time.timeScale == 0)
        {
            anim.speed = 0;
        }
        else
        {
            anim.speed = 1;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !isCrumbling)
        {
            anim.SetTrigger("PlayerContact");
            crumbleStart = Time.time;
            isCrumbling = true;
        }
    }
}
