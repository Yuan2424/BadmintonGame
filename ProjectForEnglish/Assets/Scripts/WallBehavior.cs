using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    public bool active;
    SpriteRenderer spriteRenderer;
    Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeActivity()
    {
        active = !active;
        if (active)
        {
            Color current = spriteRenderer.color;
            current.a = 1;
            spriteRenderer.color = current;
            col.enabled = true;
        }
        else
        {
            Color current = spriteRenderer.color;
            current.a = 0.25f;
            spriteRenderer.color = current;
            col.enabled = false;
        }
    }
}
