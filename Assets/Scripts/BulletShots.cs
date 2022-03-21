using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShots : MonoBehaviour
{
    //player shooting and bullet speed
    public float speed;
    Rigidbody rb;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        // bullet fire in y direction
        rb.velocity = transform.up * speed;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
