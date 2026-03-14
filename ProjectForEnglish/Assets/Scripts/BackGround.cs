using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-40, 40), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
