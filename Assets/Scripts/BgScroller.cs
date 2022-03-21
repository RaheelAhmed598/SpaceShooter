using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScroller : MonoBehaviour
{
    //bg movement speed and its start position
    public float scrollSpeed;
    private Vector3 startPosition;
    void Start()
    {
        startPosition.x = transform.position.x;
        startPosition.y = transform.position.y;
        startPosition.z = transform.position.z;
    }

  
    void Update()
    {
        //Loops the value t, so that it is never larger than length and never smaller than 0.
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, this.transform.localScale.y);
        transform.position = startPosition + Vector3.forward * newPosition;
    }
}
