using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherBehavior : MonoBehaviour
{
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
    public void activated()
    {
        Color current = spriteRenderer.color;
        current.a = 0.25f;
        spriteRenderer.color = current;
        col.enabled = false;
    }
}
