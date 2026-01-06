using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBox : MonoBehaviour
{
    GameObject player;
    PlayerMoves playerScript;
    Rigidbody2D rb;
    bool following = false;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerMoves>();
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 1.5f && playerScript.getPlayerVelocity().magnitude > 0.2f)
        {
            following = false;
            rb.velocity = (player.transform.position - transform.position).normalized * playerScript.getPlayerVelocity().magnitude * 0.95f;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > 1.5f)
        {
            following = true;
            //rb.AddForce((player.transform.position - transform.position) * 5);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        if (following)
        {
            rb.velocity = (player.transform.position - transform.position).normalized * 6;
        }
        if (Vector3.Distance(transform.position, player.transform.position) < 0.2f)
        {
            following = false;
        }
    }
    public void resetCamBox()
    {
        transform.position = startPos;
    }
}
