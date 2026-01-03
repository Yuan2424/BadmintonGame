using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    public GameObject wall;
    WallBehavior wallCode;
    SpriteRenderer spriteRenderer;
    Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        wallCode = wall.GetComponent<WallBehavior>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void activated()
    {
        wallCode.changeActivity();
        Color current = spriteRenderer.color;
        current.a = 0.25f;
        spriteRenderer.color = current;
        col.enabled = false;
    }
}
