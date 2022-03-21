using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    //aestroid random movement towards player
    // with speed and physic component 
    public float tumble;
    public Rigidbody rb;
    public float speed;
    void Start()
    {
        //agular movement of the aestroid 
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;
        rb.velocity = -transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
