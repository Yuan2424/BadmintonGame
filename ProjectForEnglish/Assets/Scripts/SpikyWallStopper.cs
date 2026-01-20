using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikyWallStopper : MonoBehaviour
{
    GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        wall = GameObject.Find("SpikyWall");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            wall.GetComponent<SpikyWall>().stopRising();
        }
    }
}
