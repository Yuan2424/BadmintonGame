using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockBehavior : MonoBehaviour
{
    public GameObject wall;
    WallBehavior wallCode;
    SpriteRenderer spriteRenderer;
    Collider2D col;
    float timer;
    bool timerSet;
    public float activateTime;
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
        if (Time.time > timer + activateTime && timerSet)
        {
            activated();
            timerSet = false;
        }
    }
    public void activated()
    {
        wallCode.changeActivity();
        Color current = spriteRenderer.color;
        if (col.enabled)
        {
            current.a = 0.25f;
        }
        else
        {
            current.a = 1;
        }
        spriteRenderer.color = current;
        col.enabled = !col.enabled;
    }
    public void setTimer()
    {
        timer = Time.time;
        timerSet = true;
    }
}
